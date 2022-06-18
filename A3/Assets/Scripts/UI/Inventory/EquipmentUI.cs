
public class EquipmentUI : InventoryUI {

    protected new void OnEnable(){
        TradeNode.OnStartTrade += () => { UpdateInventory(); };
        Inventory.OnInventoryChange += () => { UpdateInventory(); };
    }

    protected new void OnDisable(){
        TradeNode.OnStartTrade -= () => { UpdateInventory(); };
        Inventory.OnInventoryChange -= () => { UpdateInventory(); };
    }

    // Método para catualizar el inventario
    protected new void UpdateInventory() {
        ClearInventory();
        ShowInventory(Inventory);
    }

    // Método para mostrar el inventario
    // @param Inventory inv -> Inventario a mostrar
    protected new void ShowInventory(Inventory inv) {
        // Instanciamos los InventorySlotUI
        for (int i = 0; i < inv.Length; i++) {
                _shownObjects.Add(MakeNewEntry(inv.GetSlot(i)));
        }
        // Actualizamos el texto de peso y oro
        InventoryWeightText.text = inv.Weight.ToString() + " O.z.";
        InventoryGoldText.text = "Def: " + inv.CalcDefenseEquipmentOnInventory().ToString();
    }

}
