using UnityEngine;

public class EnemyHear : Enemy {

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }

    void Start() {
        _entity = FindObjectOfType<Player>().transform;
        _detectionRange = 2.5f;
        _pos = transform.position;
    }

    void FixedUpdate() {
        EntityDetection();
    }

    // Re implementación del método EntityDetection para gestionar como detectar entidades
    public new void EntityDetection(){
        
        _detect = (EntityInRange(_entity) && EntityNoiseDetector(_entity));

        if (_detect) Enemy.EnemyFound?.Invoke(transform);
        else Enemy.EnemyLost?.Invoke(transform);

    }

}