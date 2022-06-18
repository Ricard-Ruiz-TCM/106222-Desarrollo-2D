using UnityEngine;

[System.Serializable]
public class InventorySlot {
    
    [SerializeField]
    private Item _item;
    public Item GetItem() { return _item; }

    [SerializeField]
    private int _amount;
    public int GetAmount() { return _amount; }

    // Constructor del item
    public InventorySlot(Item item) {
        _item = item;
        _amount = 1;
    }

    // Constructor dle item con cantidad
    public InventorySlot(Item item, int amount) {
        _item = item;
        _amount = amount;
    }

    // Método para comproar si el slot contiene este item
    // @param ItemObject item -> objeto a comprobar
    internal bool HasItem(Item item) {
        return item == _item;
    }

    // Método para incrementar la cantidad
    internal void AddOne() {
        _amount++;
    }

    // Método para decrementar la cantidad
    internal void RemoveOne() {
        _amount--;
    }

    // Método para comprobar si el slot está vacio
    // @return bool -> true vacio | false lleno
    public bool IsEmpty() {
        return _amount < 1;
    }
}


