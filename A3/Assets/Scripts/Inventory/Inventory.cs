using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Delivery3/Inventory")]
public class Inventory : ScriptableObject {

    // Observer para gestionar cualquier modificación de los item sdel invetnario
    public static Action OnInventoryChange;

    [SerializeField]
    private List<InventorySlot> Slots;
    public int Length => Slots.Count;

    public int Gold;
    public float Weight;
    public string Owner;   

    public List<Item> GetItems(){
        List<Item> lst = new List<Item>();
        foreach (InventorySlot slt in Slots){
            lst.Add(slt.GetItem());
        }
        return lst;
    }

    // Método para añadir un item al inventario
    // @param Item item -> objeto
    public void AddItem(Item item) {
        if (Slots == null) Slots = new List<InventorySlot>();

        InventorySlot slot = GetSlot(item);
        if (slot != null && item.CanStack){
            slot.AddOne();
        } else {
            slot = new InventorySlot(item);           
            Slots.Add(slot);
        }

        OnInventoryChange?.Invoke(); CalcWeight();
    }

    // Método para quitar un item del inventario    
    // @param Item item -> objeto
    public void RemoveItem(Item item) {
        if (Slots == null) return;

        InventorySlot slot = GetSlot(item);

        if (slot != null) {
            slot.RemoveOne();
            if (slot.IsEmpty()) RemoveSlot(slot);
        }

        OnInventoryChange?.Invoke(); CalcWeight();
    }

    // Méotod para remover un slot del inventario
    // @param InventorySlot Slot -> Slot de inventairo a borrar
    private void RemoveSlot(InventorySlot slot) {
        Slots.Remove(slot);
    }
   
    // Método apra recuperar el inventoryslot contenedor de un item, si existe
    // @param Item item -> objeto
    // @return InventorySlot -> devuelve el inventory slot
    private InventorySlot GetSlot(Item item) {
        for (int i = 0; i < Slots.Count; i++) {
            if (Slots[i].HasItem(item)) return Slots[i];
        }
        return null;
    }

    // Método apra recuperar el InventorySlot
    // @param int i -> posición
    // @return InventorySlot -> devuelve el inventory slot
    public InventorySlot GetSlot(int i) {
        return Slots[i];
    }

    // Método para actualizar el peso del inventario
    private void CalcWeight(){
        Weight = 0.0f;
        for (int i = 0; i < Slots.Count; i++) {
            Weight += Slots[i].GetItem().Weight * Slots[i].GetAmount();
        }
    }

    // Método para calcular la defensa de las piezas de equipo en el inventario
    // @return int -> total de defens
    public int CalcDefenseEquipmentOnInventory(){
        int def = 0;
        for (int i = 0; i < Slots.Count; i++) {
            if (Slots[i].GetItem() is EquipmentItem){
                def += ((EquipmentItem)Slots[i].GetItem()).Arm;
            }
        }
        return def;
    }
}
