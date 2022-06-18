using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/DeathNode")]
public class DeathNode : Node, IEnterNode {

    // Observer para la reset del juego
    public static event Action OnDie;

    public void OnEnterNode(){
        OnDie?.Invoke();
    }

}