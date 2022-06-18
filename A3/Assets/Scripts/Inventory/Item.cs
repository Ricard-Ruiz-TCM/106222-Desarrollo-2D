using UnityEngine;

// Enum para determinar el tipo de objeto
public enum IType {
    I_FOOD, I_POTION, I_EQUIPMENT
}

// Scriptable Object para los objetos del juego
[CreateAssetMenu(fileName = "New Item", menuName = "Delivery3/Items/Item")]
public class Item : ScriptableObject {

    public string Name;
    public IType Type;
    public Sprite Icon;

    public float Weight;

    public int BuyCost;
    public int SellValue;

    public bool CanStack;

}
