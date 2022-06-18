using System;
using UnityEngine;

// Scriptable Object para los objetos del juego
[CreateAssetMenu(fileName = "New Potion", menuName = "Delivery3/Items/Potion")]
public class PotionItem : ConsumableItem {

    // Observer para la gestion del uso de pociones
    public static event Action<int> OnPotionUsed;

    public int Health;

    public override void Use() {
        OnPotionUsed?.Invoke(Health);
    }

}
