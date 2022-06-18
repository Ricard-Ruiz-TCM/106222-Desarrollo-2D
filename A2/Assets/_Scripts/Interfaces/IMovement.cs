
public interface IMovement {

    float Speed { get; }
    float MaxSpeed { get; }
    bool IsMoving { get; }
    bool IsSneak { get; }

    void Move();

}
