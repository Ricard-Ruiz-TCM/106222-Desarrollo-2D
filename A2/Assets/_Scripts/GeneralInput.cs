using UnityEngine;

public class GeneralInput : MonoBehaviour {

    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }
    public bool LeftShift { get; private set; }

    void OnEnable(){
        PlayerMovement.InputHorizontal += () => { return HorizontalAxis; };
        PlayerMovement.InputVertical += () => { return VerticalAxis; };
        PlayerMovement.InputLeftShift += () => { return LeftShift; };
    }

    void OnDisable(){
        PlayerMovement.InputHorizontal -= () => { return HorizontalAxis; };
        PlayerMovement.InputVertical -= () => { return VerticalAxis; };
        PlayerMovement.InputLeftShift -= () => { return LeftShift; };
    }

    void Awake(){
        HorizontalAxis = 0.0f;
        VerticalAxis = 0.0f;
        LeftShift = false;
    }

    void Update() {
        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");
        LeftShift = Input.GetKey(KeyCode.LeftShift);
    }

}
