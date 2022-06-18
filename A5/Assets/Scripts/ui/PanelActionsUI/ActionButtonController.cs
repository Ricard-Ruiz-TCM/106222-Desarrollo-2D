using System.Collections.Generic;
using UnityEngine;

public class ActionButtonController : MonoBehaviour {

    public List<FightCommandTypes> _possibleCommands;
    public CombatManager _combatManager;

    private CanvasGroup _canvasGroup;
    public ActionButton _commandButtonPrefab;
    private List<GameObject> _commandButtons;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable() {
        EntityManager.OnNextEntity += SetUpButtons;
        EntityManager.OnSetPreviousEntity += SetUpButtons;
    }

    void OnDisable() {
        EntityManager.OnNextEntity -= SetUpButtons;
        EntityManager.OnSetPreviousEntity -= SetUpButtons;
    }

    internal void ChooseTarget(Entity activeEntity) {
        Hide();
    }

    void Show() {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    void Hide() {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    private void SetUpButtons(Entity entity) {

        Show();

        if (_commandButtons == null) _commandButtons = new List<GameObject>();

        // Recuperamos todos los posible commands
        _possibleCommands = ((Fighter)entity).PossibleCommands;

        // Destruimos todos los botones del turno anterior
        foreach (GameObject item in _commandButtons) {
            if(item) GameObject.Destroy(item);
        }
        _commandButtons.Clear();

        // Instanciamos los botones de comandos nuevos
        for (int i = 0; i < _possibleCommands.Count; i++){
            ActionButton btn = Instantiate(_commandButtonPrefab);
            btn.transform.SetParent(this.transform);
            btn.Load(_possibleCommands[i], this);
            _commandButtons.Add(btn.gameObject);
        }

    }
    
    public void OnButtonPressed(FightCommandTypes fightCommandType) {
        _combatManager.DoAction(fightCommandType);
    }
}