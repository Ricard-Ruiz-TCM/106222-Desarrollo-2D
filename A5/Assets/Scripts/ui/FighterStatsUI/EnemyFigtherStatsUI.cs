using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFigtherStatsUI : SingleFighterStatsUI {

    Vector2 pos;

    void Start(){
        pos = transform.position;
    }

    void Update(){

        Entity e = (Entity)FindEntity();
        if (e != null){
            if (e.Team == Team.Enemy){
                UpdateStats(e);
                Show();
            }
        } else {
            Hide();
        }

    }

    public void Show(){
        transform.position = pos;
    }

    public void Hide(){
        transform.position = new Vector2(0.0f, -200.0f);
    }   

    ISelectable FindEntity() {

        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData) {
            var target = hitData.collider.GetComponent<ISelectable>();
            if (target != null) 
                return target;
        }
        return null;
    }


}
