using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Nodo inicial del sistema de dialogo
    [SerializeField]
    private Node _startNode;

    // Observer para reiniicar le juego completamente
    public static event Action GameReset;

    void Start(){
        DialogManager.StartConversation(_startNode);
    }

    public void OnClickReset(){
        GameReset?.Invoke();
    }

}