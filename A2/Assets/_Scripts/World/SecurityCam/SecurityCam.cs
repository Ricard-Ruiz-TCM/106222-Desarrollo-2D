using UnityEngine;

public class SecurityCam : EnemySee {

    void Start() {
        
        _entity = FindObjectOfType<Player>().transform;
        _visionAngle = 45.0f;
        _detectionRange = 7.50f;

        _speed = 0.25f;
        _maxSpeed = _speed;

        _pos = transform.position;

    }

    // Método para mover al personaje según vector forward, _speed y dt
    public override void Move(){
        transform.Rotate(0.0f, 0.0f, _speed);
    }

    // Método para rotar al personaje cuando se acerca a una pared
    public override void ChangeDirection(Transform me){
        if (me == transform) _speed = -_speed;
    }

}