using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRest : MonoBehaviour {

    private PlayerStats _stats;
    private Inventory _inventory;

    void OnEnable(){
        RestNode.OnRest += Rest;
    }

    void OnDisable(){
        RestNode.OnRest -= Rest;
    }

    void Awake(){
        _stats = GetComponent<PlayerStats>();
        _inventory = GetComponent<Player>()._data._inventory;
    }

    // Método para recuperar la salud comiendo la comida que tenga el jugadro en el inventario
    public void Rest(){
        int index = 0;
        while((_stats.NeedToHeal()) && (index < _inventory.Length)){
            Item it = _inventory.GetSlot(index).GetItem();
            if (it is FoodItem) ((FoodItem)it).Use();
            index++;
        }
    }

}
