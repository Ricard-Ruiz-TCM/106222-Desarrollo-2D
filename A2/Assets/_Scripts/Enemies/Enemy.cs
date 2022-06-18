using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDetectEntities<Transform>, IReseteableEntity {

    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    // IDetectEntities
    public Transform Entity => _entity;
    public bool EntityDetected => _detect;
    public float VisionAngle => _visionAngle;
    public float DetectionRange => _detectionRange;
    //IReseteableentity
    public Vector2 InitialPosition => _pos;

    protected Transform _entity;
    [SerializeField]
    protected bool _detect;
    [SerializeField]
    protected float _visionAngle;
    [SerializeField]
    protected float _detectionRange;

    protected Vector2 _pos;

    // Observers cuando el enemigo es detectado y cuando el enemigo sale de la detección
    public static Action<Transform> EnemyFound;
    public static Action<Transform> EnemyLost;

    void OnEnable(){
        GameManager.Reset += ResetPos; 
    }
    
    void OnDisable(){
        GameManager.Reset -= ResetPos; 
    }

    void Start(){
        
        _entity = null;
        _detect = false;
        _visionAngle = 0.0f;
        _detectionRange = 0.0f;

    }

    // Método para detectar enemigos
    // Implementado en los hijos
    public virtual void EntityDetection(){}

    // Método para comprobar si la entidad esta en rango
    // @param Transform item -> Transform de la entidad
    // @return bool -> true en rango, false fuera de rango
    public bool EntityInRange(Transform entity){
        return (Vector2.Distance(entity.position, transform.position) < _detectionRange);
    }

    // Método para comprobar si la entidad esta en POV
    // @param Transform item -> Transform de la entidad
    // @return bool -> true en POV, false fuera de POV
    public bool EntityInPOV(Transform entity){
        Vector2 v1 = transform.right;
        Vector2 v2 = entity.position - transform.position;
        float angle = Vector2.Angle(v1, v2);
        return (_visionAngle >= 2*angle);
    }

    // Método para comprobar si la entidad es alcanzable por un raycast
    // @param Transform item -> Transform de la entidad
    // @return bool -> true con detección de raycast, false sin detección de raycast
    public bool EntityIsVisible(Transform entity){
        Vector3 dir = entity.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _detectionRange * 100.0f);
        return hit.collider.transform == entity.transform;
    }

    // Método para comprobar si la entidad puede ser escuchada
    // @param Transform item -> Transform de la entidad
    // @return bool -> true si la escucha, false si no la escucha
    public bool EntityNoiseDetector(Transform entity){
        if (entity.gameObject.GetComponent<Player>() != null){
            return (entity.GetComponent<PlayerMovement>().Speed >= entity.GetComponent<PlayerMovement>().MaxSpeed / 1.75f);
        }
        return false;
    }

    // Método para reiniciar nuestra posición a la posición inicial
    public void ResetPos(){
        transform.position = InitialPosition;
    }
}
