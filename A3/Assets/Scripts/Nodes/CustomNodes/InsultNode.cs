using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/InsultNode")]
public class InsultNode : Node, IEnterNode, IExitNode {

    // Observer para la ejecución del insulto
    public static event Action OnInsult;

    public void OnEnterNode(){
        if (!FindObjectOfType<Shopkeeper>().ImTilted()) _canBack = true;
    }

    public void OnExitNode(){
        OnInsult?.Invoke();
        _canBack = false;
    }

}