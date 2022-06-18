using UnityEngine;

public class Key : MonoBehaviour, IReseteableEntity {

    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    // IReseteableEntity
    public Vector2 InitialPosition => _pos;

    private Vector2 _pos;

    void OnEnable(){
        PlayerCollision.PicKey += VanishMe;
        GameManager.Reset += ResetPos;
    }

    void OnDisable(){
        PlayerCollision.PicKey -= VanishMe;
        GameManager.Reset -= ResetPos;
    }

    void Start(){
        _pos = transform.position;
    }

    // Método para mandar a la llave fuera de la escena
    public void VanishMe(){
        transform.position = new Vector3(transform.position.x + 1000.0f, transform.position.y, transform.position.z);
    }

    // Método para reiniciar nuestra posición a la posición inicial
    public void ResetPos() {
        transform.position = InitialPosition;
    }

}
