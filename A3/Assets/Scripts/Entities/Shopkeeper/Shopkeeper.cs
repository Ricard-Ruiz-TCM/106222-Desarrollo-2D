using UnityEngine;

public class Shopkeeper : Entity {

    [SerializeField]
    private bool _tilted = false;
    public bool ImTilted() { return _tilted; }

    void OnEnable(){
        InsultNode.OnInsult += Tilt;
        GameManager.GameReset += Reset;
    }

    void OnDisable(){
        InsultNode.OnInsult -= Tilt;
        GameManager.GameReset -= Reset;
    }

    // Método para enfadar al vendedor
    public void Tilt(){
        _tilted = true;
    }

    // Método Reset
    public void Reset(){
        _tilted = false;
    }
    
}
