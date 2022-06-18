using UnityEngine;

public class BasicCameraController : MonoBehaviour {

    [SerializeField]
    GameObject _entity;

    [SerializeField]
    private bool _focusDoor;
    public bool IsFocusingDoor(){ return _focusDoor; }

    void OnEnable(){
        PlayerCollision.PicKey += FocusDoor;
    }

    void OnDisable(){
        PlayerCollision.PicKey -= FocusDoor;
    }

    void Start(){
        _focusDoor = false;
        _entity = FindObjectOfType<Player>().gameObject;
    }

    void Update(){
        if (!IsFocusingDoor()){
            transform.position = new Vector3(_entity.transform.position.x, _entity.transform.position.y, transform.position.z);
            GetComponent<Camera>().orthographicSize = (_entity.GetComponent<PlayerMovement>().IsSneak ? 5 : 6);
        }
    }

    // Método para hacer focus a la puerta mientras se abre, con callback de 2.0f segundso a dejar de focusearla
    public void FocusDoor(){
        _focusDoor = true;
        Vector3 doorPos = FindObjectOfType<DoorOpener>().transform.position;
        transform.position = new Vector3(doorPos.x, doorPos.y, transform.position.z);
        Invoke("ComeBackToPlayer", 2.0f);
    }

    // Método para volver al player, básicamente
    private void ComeBackToPlayer(){
        _focusDoor = false;
    }

}
