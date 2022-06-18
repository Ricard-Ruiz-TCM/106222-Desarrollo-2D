using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/SellNode")]
public class SellNode : Node, IExitNode {

    // Observer para la venta de objetos
    public static event Action OnSelectSell;

    public void OnExitNode(){
        OnSelectSell?.Invoke();
    }

}