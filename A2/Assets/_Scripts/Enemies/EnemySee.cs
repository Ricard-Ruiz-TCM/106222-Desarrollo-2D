using UnityEngine;

public class EnemySee : Enemy, IMovement {

    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    // IMovement
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public bool IsMoving => _isMoving;
    public bool IsSneak => _isSneak;

    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _maxSpeed;
    [SerializeField]
    protected bool _isMoving;
    [SerializeField]
    protected bool _isSneak;

    void OnEnable(){
        EnemyCollision.Close2Wall += ChangeDirection;
        GameManager.Reset += ResetPos;
    }

    void OnDisable(){
        EnemyCollision.Close2Wall -= ChangeDirection;
        GameManager.Reset -= ResetPos;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        Gizmos.color = Color.red; 
        var direction = Quaternion.AngleAxis(_visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * _detectionRange);
        var direction2 = Quaternion.AngleAxis(-_visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * _detectionRange); 
    }

    void Start() {

        _speed = 1.5f;
        _maxSpeed = _speed;
        _isMoving = true;
        _isSneak = false;

        _entity = FindObjectOfType<Player>().transform;
        _visionAngle = 90.0f;
        _detectionRange = 3.15f;

        _pos = transform.position;

    }

    void FixedUpdate(){
        Move();
        EntityDetection();
    }

    // Re implementación del método EntityDetection para gestionar como detectar entidades
    public override void EntityDetection(){
        
        _detect = (EntityInRange(_entity) && EntityInPOV(_entity) && EntityIsVisible(_entity));

        if (_detect) Enemy.EnemyFound?.Invoke(transform);
        else Enemy.EnemyLost?.Invoke(transform);

    }

    // Método para mover al personaje según vector forward, _speed y dt
    // Método virtual para hacer override en el EnemySeek
    public virtual void Move(){
        transform.position += transform.right * _speed * Time.fixedDeltaTime;
    }

    // Método para rotar al personaje cuando se acerca a una pared
    // @param Transform me -> transform del invokador del
    public virtual void ChangeDirection(Transform me){
        if (me == transform) transform.Rotate(0.0f, 0.0f, Random.Range(90.0f, 270.0f));
    }

}