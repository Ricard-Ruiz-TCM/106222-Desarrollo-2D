using UnityEngine;

// Scriptable Object para la gestion de los personajes
[CreateAssetMenu(fileName = "New Character", menuName = "Delivery3/Characters", order = 1)]
public class EntityData : ScriptableObject {

    public string _name;
    public Inventory _inventory;

}
