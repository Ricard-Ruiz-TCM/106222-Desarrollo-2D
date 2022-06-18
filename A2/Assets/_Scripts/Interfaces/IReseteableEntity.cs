using UnityEngine;

public interface IReseteableEntity {

    public Vector2 InitialPosition { get; }

    public void ResetPos();

}
