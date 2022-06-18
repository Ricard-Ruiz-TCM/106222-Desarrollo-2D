using TMPro;
using UnityEngine;

public class CombatCommandTextUI : MonoBehaviour {

    public TextMeshProUGUI _text;

    // Set del texto para la linea de texto
    public void SetText(string text){
        _text.text = text;
    }
}
