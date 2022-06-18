using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement {

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

    private Rigidbody2D _rigidbody;

    // Observers para el input necesario del player
    public delegate float IH(); 
    public static event IH InputHorizontal;

    public delegate int Metd();
    public static event Metd hola;

    int a = hola?.Invoke();

    void OnEnable(){
        Clase.Metd += MiMetodo;
    }

    public int MiMetodo(){
        return 16;
    }



    public delegate float IV(); 
    public static event IV InputVertical;
    public delegate bool ILS(); 
    public static event ILS InputLeftShift;
    
    void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start(){
        _speed = 5.0f;
        _maxSpeed = _speed;
        _isMoving = false;
        _isSneak = false;
    }
   
    void FixedUpdate() {
        Move();
    }

    // Método para gestioonar el movimiento, basando en el input y movido por el rigidbody
    // También controla la dirección donde mirar y la reinicia
    public void Move(){
        
        _isSneak = (bool)(InputLeftShift?.Invoke());
        Vector2 direction = new Vector2((float)(InputHorizontal?.Invoke()), (float)(InputVertical?.Invoke())) * (_isSneak ? _maxSpeed/2.0f : _maxSpeed);
        _rigidbody.velocity = direction;
        _speed = (Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(_rigidbody.velocity.y));
        _isMoving = direction.magnitude > 0.01f;

        if (_isMoving) LookAt((Vector2)transform.position + direction);
        else transform.rotation = Quaternion.identity;

    }

    // Método para rotar al personaje y hacerlo "mirar" hacia nosotros
    // @param Vector2 targetPosition -> Dirección de rotación
    void LookAt(Vector2 targetPosition) {
        float angle = 0;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    
}
