using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleFighterStatsUI : MonoBehaviour {

    public Image _health;
    public TextMeshProUGUI _healthTXT;
    public TextMeshProUGUI _defense;
    public TextMeshProUGUI _attack;

    public void UpdateStats(Entity ent){

        Fighter fighter = (Fighter)ent;

        _defense.text = fighter.Defense.ToString();
        _attack.text = fighter.Attack.ToString();

        _healthTXT.text = "HP: " + fighter.CurrentHealth.ToString();
        
        float w = _health.transform.parent.GetComponent<RectTransform>().rect.width;
        _health.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0.0f, 
            ((fighter.CurrentHealth / fighter.MaxHealth) * w));
    }

}
