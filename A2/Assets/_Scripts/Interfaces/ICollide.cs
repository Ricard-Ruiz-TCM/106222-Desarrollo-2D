using System.Collections.Generic;
using UnityEngine;

public interface ICollide {

    public float CheckRadius { get; }

    public List<Transform> CheckPoints { get; }

    public bool CheckCollisions(LayerMask layer);

    public bool RCollision(LayerMask layer, List<Transform> list, int pos);

}
