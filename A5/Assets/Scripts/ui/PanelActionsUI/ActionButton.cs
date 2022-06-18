using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler {

    private Color _color;
    private Image _image;
    private TextMeshProUGUI _text;

    private ActionButtonController _buttonController;
    private FightCommandTypes _type;

    public void OnPointerClick(PointerEventData eventData) {
        _buttonController.OnButtonPressed(_type);
    }
    
    void Awake() {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Load(FightCommandTypes type, ActionButtonController colorButtonController) {
        _type = type;
        _buttonController = colorButtonController;
        _text.text = type.ToString(); ;
    }


}