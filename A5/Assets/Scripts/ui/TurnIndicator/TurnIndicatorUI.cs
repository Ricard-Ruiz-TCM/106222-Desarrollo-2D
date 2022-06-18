using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicatorUI : MonoBehaviour {

    public EntityManager EntityManager;

    public Image Active;
    public Image Next1;
    public Image Next2;

    void Update(){
        UpdateTurn();
    }

    public void UpdateTurn(){

        SetImage(Active, EntityManager.ActiveEntity);
        SetImage(Next1, EntityManager.NextEntity);
        SetImage(Next2, EntityManager.Next2Entity);

    }

    private void SetImage(Image pos, Entity ent){
        pos.sprite = ent.GetComponent<SpriteRenderer>().sprite;
    }

}
