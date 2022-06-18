using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/TradeNode")]
public class TradeNode : Node, IEnterNode {

    // Observer para activar el sistema de compra venta
    public static event Action OnStartTrade;
    public static event Action OnExitTrade;

    public void OnEnterNode(){
        OnStartTrade?.Invoke();
    }

    public void CloseTradeNode(){
        OnExitTrade?.Invoke();
    }

}