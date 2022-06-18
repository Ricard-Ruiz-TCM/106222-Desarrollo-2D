using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionNode", menuName = "Delivery3/Nodes/EquipmentNode")]
public class EquipmentNode : Node, IExitNode {

    // Observer para la compra venta de equipo
    public static event Action OnSelectEquipment;

    public void OnExitNode(){
        OnSelectEquipment?.Invoke();
    }
    
}