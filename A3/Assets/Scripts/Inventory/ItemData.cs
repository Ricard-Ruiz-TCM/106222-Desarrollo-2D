using UnityEngine;
using System.Collections.Generic;

public class ItemData : MonoBehaviour {

    // Singleton a modo de contenedor para todos los items del juego
    public static ItemData Instance;

    [SerializeField]
    private List<Item> _items;

    void Awake() {
        if (Instance == null) Instance = this;
    }

    // Método para recupear todos los items
    // @return List<Item> -> Lista de items
    internal static List<Item> GetItems(){
        return Instance.IGetItems();
    }

    // Método para recupear todos los items
    // @return List<Item> -> Lista de items
    List<Item> IGetItems(){
        return _items;
    }

    // Método para genera un item aleatorio entre la totalidad de items que existen
    // @return Item -> objeto generado
    internal static Item GetRandomItem(){
        return Instance.IGetItems()[Random.Range(0, Instance.IGetItems().Count - 1)];
    }

}