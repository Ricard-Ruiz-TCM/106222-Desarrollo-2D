using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Node", menuName = "Delivery3/Nodes/Node", order = 1)]
public class Node : ScriptableObject {

    public int _id;
    public string _speechText;

    public string _speakerText;
    public List<NodeOption> _options;

    public Node _parentNode;

    public bool _canBack;

}
