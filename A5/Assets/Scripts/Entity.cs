using UnityEngine;

public enum Team{
    Player, 
    Enemy,
}

public class Entity : MonoBehaviour, ISelectable {
    
    public Team Team;

    public void ClickedBad() {}

    public void ClickedGood() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void HighlightBad() {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void HighlightGood() {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void UnSelect() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
