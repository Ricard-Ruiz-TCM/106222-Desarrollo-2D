using UnityEngine;

public class Player : MonoBehaviour, IReseteableEntity {

    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    //IReseteableentity
    public Vector2 InitialPosition => _pos;

    private Vector2 _pos;

    // Habilitamos el observer de GameManager para gestionar el reset del nivel
    void OnEnable(){
        GameManager.Reset += ResetPos;
    }
    void OnDisable(){
        GameManager.Reset -= ResetPos;
    }

    void Start(){
        _pos = transform.position;
    }

    // Método para reiniciar nuestra posición a la posición inicial
    public void ResetPos() {
        transform.position = InitialPosition;
    }
}
