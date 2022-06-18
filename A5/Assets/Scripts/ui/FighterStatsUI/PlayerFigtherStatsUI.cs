using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFigtherStatsUI : SingleFighterStatsUI {

    [SerializeField]
    private TextMeshProUGUI _text;

    void OnEnable(){
        EntityManager.OnNextEntity += UpdateStats;
        EntityManager.OnNextEntity += ChangeTurnName;
    }

    void OnDisable(){
        EntityManager.OnNextEntity -= UpdateStats;
        EntityManager.OnNextEntity -= ChangeTurnName;
    }

    void ChangeTurnName(Entity e){
        _text.text = e.gameObject.name;
    }

}
