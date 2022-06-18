using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Observer para identificar si toca hacer reset
    public static Action Reset;

    void OnEnable(){
        EnemyCollision.Close2Player += ResetGame;
    }

    void OnDisable(){
        EnemyCollision.Close2Player -= ResetGame;
    }

    // Método para resetear el juego solo si la camara no esta focuseando la puerta
    // Todavía pdoría dejar de focusear a la puerta y morir isntantaneamente si hay un
    // EnemySeek cerca, pero no se reiniciaría el nivel mientras se hace la animación
    // Da un poco de palo implementar más gestion de escenario y nivel para evitar ese caso. sorry :/
    public void ResetGame(){
        if (!FindObjectOfType<BasicCameraController>().IsFocusingDoor()) Reset?.Invoke();
    }

}
