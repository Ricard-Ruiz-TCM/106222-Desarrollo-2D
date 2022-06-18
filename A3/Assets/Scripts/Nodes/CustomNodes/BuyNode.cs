using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Node", menuName = "Delivery3/Nodes/BuyNode")]
public class BuyNode : Node, IExitNode {

    // Observer para la compra de objetos
    public static event Action OnSelectBuy;

    public void OnExitNode(){
        OnSelectBuy?.Invoke();
    }
    
}