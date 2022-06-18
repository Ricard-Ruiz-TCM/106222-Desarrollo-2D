using UnityEngine;

// Enum para determinar el subtypo de los ITEM_EQUIPMENT
public enum IEType {
    NO_EQUIPMENT, ET_WEAPON, ET_HELMET, ET_ARMOR, ET_LEG, ET_BOOTS, ET_SHIELD
}

// Scriptable Object para los objetos del juego
[CreateAssetMenu(fileName = "New Equip", menuName = "Delivery3/Items/Equip")]
public class EquipmentItem : Item {

    public IEType EType;

    public int Arm;
    public int Dmg;

}
