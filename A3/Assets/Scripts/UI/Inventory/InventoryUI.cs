using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    // Inventario
    public Inventory Inventory;
    // Prefabs del slot de inventario
    public InventorySlotUI InventorySlotPrefab;

    // Textos para el peso y dinero dle inventario
    public TextMeshProUGUI InventoryWeightText;
    public TextMeshProUGUI InventoryGoldText;

    // Lista de GameObjects contenedora de la interfaz
    protected List<GameObject> _shownObjects;

    [SerializeField]
    private IType _currentTradingItemType;

    protected void OnEnable(){
        TradeNode.OnStartTrade += () => { UpdateInventory(); };
        Inventory.OnInventoryChange += () => { UpdateInventory(); };
        PotionsNode.OnSelectPotion += () => { _currentTradingItemType = IType.I_POTION; };
        FoodNode.OnSelectFood += () => { _currentTradingItemType = IType.I_FOOD; };
        EquipmentNode.OnSelectEquipment += () => { _currentTradingItemType = IType.I_EQUIPMENT; };
    }

    protected void OnDisable(){
        TradeNode.OnStartTrade -= () => { UpdateInventory(); };
        Inventory.OnInventoryChange -= () => { UpdateInventory(); };
        PotionsNode.OnSelectPotion -= () => { _currentTradingItemType = IType.I_POTION; };
        FoodNode.OnSelectFood -= () => { _currentTradingItemType = IType.I_FOOD; };
        EquipmentNode.OnSelectEquipment -= () => { _currentTradingItemType = IType.I_EQUIPMENT; };
    }

    // Método para mostrar el inventario
    // @param Inventory inv -> Inventario a mostrar
    protected void ShowInventory(Inventory inv) {
        // Instanciamos los InventorySlotUI
        for (int i = 0; i < inv.Length; i++) {
            if (inv.GetSlot(i).GetItem().Type == _currentTradingItemType)
                _shownObjects.Add(MakeNewEntry(inv.GetSlot(i)));
        }
        // Actualizamos el texto de peso y oro
        InventoryWeightText.text = inv.Weight.ToString() + " O.z.";
        InventoryGoldText.text = inv.Gold.ToString();
    }

    // Método para crear un nuevo InventorySlot en el inventario
    protected GameObject MakeNewEntry(InventorySlot inventorySlot) {
        InventorySlotUI element = GameObject.Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
        element.LoadData(inventorySlot,this);
        return element.gameObject;
    }

    // Método para catualizar el inventario
    protected void UpdateInventory() {
        ClearInventory();
        ShowInventory(Inventory);
    }

    // Método para borrar todos los items del inventario
    protected void ClearInventory() {
        // Control sobre la primera ejecución
        if (_shownObjects == null) _shownObjects = new List<GameObject>();

        foreach (var item in _shownObjects) {
            if(item) Destroy(item);
        }
        _shownObjects.Clear();
    }

}
