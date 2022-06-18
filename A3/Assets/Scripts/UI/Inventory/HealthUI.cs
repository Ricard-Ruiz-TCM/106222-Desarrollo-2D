using UnityEngine;

public class HealthUI : MonoBehaviour {

    [SerializeField]
    private PlayerStats _stats;

    void OnEnable(){
        TradeNode.OnStartTrade += () => { UpdateHealth(); };
    }

    void OnDisable(){
        TradeNode.OnStartTrade -= () => { UpdateHealth(); };
    }

    // Método para actualizar la vida en la barrita
    public void UpdateHealth(){
        GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0.0f, _stats.Health);
    }


}