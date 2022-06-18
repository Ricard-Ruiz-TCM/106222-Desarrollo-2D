using UnityEngine;

public class DoorOpener : MonoBehaviour, IMovement, IReseteableEntity {

    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    // IMovement
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public bool IsMoving => _isMoving;
    public bool IsSneak => _isSneak;
    //IReseteableentity
    public Vector2 InitialPosition => _pos;

    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _maxSpeed;
    [SerializeField]
    protected bool _isMoving;
    [SerializeField]
    protected bool _isSneak;

    protected Vector2 _pos;

    void OnEnable(){
        PlayerCollision.PicKey += OpenDoor;
        GameManager.Reset += ResetPos;
    }

    void OnDisable(){
        PlayerCollision.PicKey -= OpenDoor;
        GameManager.Reset -= ResetPos;
    }

    void Start(){
        
        _speed = 2.0f;
        _maxSpeed = _speed;
        _isMoving = false;
        _isSneak = false;

        _pos = transform.position;
    }
    
    void FixedUpdate(){
        if (IsMoving){
            Move();
        }
    }

    // Método apra abrir la puerta y aciavr su movimiento, con un callback de 2.5f segundos para pararla
    public void OpenDoor(){
        _isMoving = true;
        Invoke("StopDoor", 2.5f);
    }

    // Método para parar el movimiento de la puerta
    private void StopDoor(){
        _isMoving = false;
    }

    // Método para mover la puerta según velocidad
    public void Move() {
        transform.position -= new Vector3(Speed, 0.0f, 0.0f) * Time.deltaTime;
    }

    // Método para reiniciar nuestra posición a la posición inicial
    public void ResetPos() {
        transform.position = InitialPosition;
    }
}
