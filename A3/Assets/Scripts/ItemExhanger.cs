using System;
using UnityEngine;

public class ItemExhanger : MonoBehaviour {

    public static event Action<Item, bool> OnBuyItem;
    public static event Action<Item, bool> OnSellItem;
    public static event Action<Item> OnEquipItem;
    public static event Action<Item> OnUnequipItem;

    [SerializeField]
    private TradingType _tradingType;

    void OnEnable(){
        InventorySlotUI.OnTradeItem += TradeItem;
        BuyNode.OnSelectBuy += () => { _tradingType = TradingType.TRADING_BUY; };
        SellNode.OnSelectSell += () => { _tradingType = TradingType.TRADING_SELL; };
    }

    void OnDisable(){
        InventorySlotUI.OnTradeItem -= TradeItem;
        BuyNode.OnSelectBuy -= () => { _tradingType = TradingType.TRADING_BUY; };
        SellNode.OnSelectSell -= () => { _tradingType = TradingType.TRADING_SELL; };
    }

    public void TradeItem(InventoryUI origin, InventoryUI destiny, Item item){
        string OOwner = origin.Inventory.Owner;
        string DOwner = destiny.Inventory.Owner;

        if (OOwner == "Player"){
            if (DOwner == "Shopkeeper"){
                if (_tradingType == TradingType.TRADING_SELL){
                    OnSellItem?.Invoke(item, SellItem(origin, destiny, item));
                }
            }
            if (DOwner == "PlayerEquip"){
                if (item is EquipmentItem) {
                    EquipItem(origin, destiny, (EquipmentItem)item);
                }
            }
        }
        else if (OOwner == "Shopkeeper"){
            if (DOwner == "Player"){
                if (_tradingType == TradingType.TRADING_BUY){
                    OnBuyItem?.Invoke(item, BuyItem(origin, destiny, item));
                }
            }
        }
        else if (OOwner == "PlayerEquip"){
            if (DOwner == "Player"){
                UnequipItem(origin, destiny, item);
            }
        }
    }

    private bool BuyItem(InventoryUI origin, InventoryUI destiny, Item item){
        if (destiny.Inventory.Gold >= item.BuyCost){
            TradeItem(origin.Inventory, destiny.Inventory, item, item.BuyCost);
            return true;
        }
        return false;
    }

    private bool SellItem(InventoryUI origin, InventoryUI destiny, Item item){
        if (destiny.Inventory.Gold >= item.SellValue){
            TradeItem(origin.Inventory, destiny.Inventory, item, item.SellValue);
            return true;
        }
        return false;
    }

    private void EquipItem(InventoryUI origin, InventoryUI destiny, EquipmentItem item){
        bool unequip = false;

        foreach (Item it in destiny.Inventory.GetItems()){
            if ((it is EquipmentItem) && (((EquipmentItem)it).EType == item.EType)){
                TradeItem(origin.Inventory, destiny.Inventory, item);
                UnequipItem(destiny, origin, it);
                unequip = true;
                break;
            }
        }

        if (!unequip) TradeItem(origin.Inventory, destiny.Inventory, item);

        OnEquipItem?.Invoke(item);
    }

    private void UnequipItem(InventoryUI origin, InventoryUI destiny, Item item){
        TradeItem(origin.Inventory, destiny.Inventory, item);
        OnUnequipItem?.Invoke(item);
    }

    private void TradeItem(Inventory origin, Inventory destiny, Item item, int value = 0){
        origin.RemoveItem(item);
        destiny.AddItem(item);
        origin.Gold += value;
        destiny.Gold -= value;
    }

}
