using UnityEngine;

public class EnemyAlarm : MonoBehaviour {
    
    SpriteRenderer _alarmRenderer;

    void OnEnable(){
        Enemy.EnemyFound += PlayerDetected;
        Enemy.EnemyLost += PlayerLeft;
    }
    void OnDisable(){
        Enemy.EnemyFound -= PlayerDetected;
        Enemy.EnemyLost -= PlayerLeft;
    }

    void Awake(){
        _alarmRenderer = GetComponent<SpriteRenderer>();
    }

    // Método para cambiar el color a rojo si detectamos player
    // @param Transform parent -> transform del invocador
    public void PlayerDetected(Transform parent) {
        if (parent == transform.parent) ChangeColor(Color.red);
    }

    // Método para resetear el color si perdemos de vista al player
    // @param Transform parent -> transform del invocador
    public void PlayerLeft(Transform parent) {
        if (parent == transform.parent) ChangeColor(new Color(0,0,0,0));
    }

    // Método para cambiar el color del sprite
    // @param Color color -> nuevo color
    private void ChangeColor(Color color) {
        _alarmRenderer.color = color;
    }

}
