using UnityEngine;
using System.Collections.Generic;

public class Player : Entity {

    public Inventory Equip => _equip;

    [SerializeField]
    private Inventory _equip;

    void OnEnable(){
        Rewards.OnRewardsGenerated += GetRewards;
        GameManager.GameReset += Reset;
    }

    void OnDisable(){
        Rewards.OnRewardsGenerated -= GetRewards;
        GameManager.GameReset -= Reset;
    }

    // Método para guardr las recompensas generadas en el inventario
    // @param List<InventorySlot> rewards -> Objetos para recibir
    public void GetRewards(List<InventorySlot> rewards){
        foreach(InventorySlot item in rewards){
            _data._inventory.AddItem(item.GetItem());
        }
    }

    // Método Reset
    public void Reset(){

    }

}
