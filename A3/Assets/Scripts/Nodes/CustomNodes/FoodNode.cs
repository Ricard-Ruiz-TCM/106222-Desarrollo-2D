using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/FoodNode")]
public class FoodNode : Node, IExitNode {

    // Observer para la compra venta de comida
    public static event Action OnSelectFood;

    public void OnExitNode(){
        OnSelectFood?.Invoke();
    }
    
}