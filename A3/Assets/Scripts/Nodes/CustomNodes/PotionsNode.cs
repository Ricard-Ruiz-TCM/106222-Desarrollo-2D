using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/PotionsNode")]
public class PotionsNode : Node, IExitNode {

    // Observer para la compra venta de pociones
    public static event Action OnSelectPotion;

    public void OnExitNode(){
        OnSelectPotion?.Invoke();
    }
    
}