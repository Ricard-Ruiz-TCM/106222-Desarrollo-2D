using System;
using UnityEngine;

// Scriptable Object para los objetos del juego
[CreateAssetMenu(fileName = "New Food", menuName = "Delivery3/Items/Food")]
public class FoodItem : ConsumableItem {

    // Observer para la gestion del uso de pociones
    public static event Action<int> OnFoodEaten;

    public int Regeneration;

    public override void Use() {
        OnFoodEaten?.Invoke(Regeneration);
    }
}
