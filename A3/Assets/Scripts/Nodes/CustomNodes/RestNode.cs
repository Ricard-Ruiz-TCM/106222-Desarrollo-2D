using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/RestNode")]
public class RestNode : Node, IEnterNode {

    // Observer para controlar el descansar
    public static event Action OnRest;

    public void OnEnterNode(){
        OnRest?.Invoke();
    }

}