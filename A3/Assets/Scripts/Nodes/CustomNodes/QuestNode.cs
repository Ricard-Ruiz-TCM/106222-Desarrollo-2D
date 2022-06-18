using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/QuestNode")]
public class QuestNode : Node, IEnterNode {

    // Observer para empezar la quest
    public static event Action OnStartQuest;

    // Observer para pillar los items
    public static event Action OnPickRewards;

    public void OnEnterNode(){
        OnStartQuest?.Invoke();
    }

    public void PickRewards(){
        OnPickRewards?.Invoke();
    }

}