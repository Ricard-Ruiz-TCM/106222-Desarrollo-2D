using System;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public EntityManager _entityManager;
    public ActionButtonController _buttonController;
    public ChooseTarget _targetChooser;
    public Invoker _invoker;

    private CommandFactory _factory;    
    private FightCommandTypes _currentFCT;

    public static event Action<string, string, string> OnExecuteCombatCommand;

    public static event Action OnUndoCombatCommand;

    void OnEnable(){
        
    }

    void OnDisable(){
    }

    void Start() {
        _factory = new CommandFactory();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) Undo();
    }

    public void DoAction(FightCommandTypes commandType) {
        _currentFCT = commandType;
        ChoosteTarget((FightCommand)_factory.GetCommand(commandType));
    }
    
    private void ChoosteTarget(FightCommand _currentCommand) {
        var targetTypes = _currentCommand.PossibleTargets;
        _entityManager.SetEntitiesFriendship();
        Entity[] possibleTargets;
        switch (targetTypes) {
            case TargetTypes.Enemy:
                possibleTargets = _entityManager.Enemies.ToArray();
                break;
            case TargetTypes.Friend:
                possibleTargets = _entityManager.Friends.ToArray();
                break;
            case TargetTypes.FriendNotSelf:
                possibleTargets = _entityManager.FriendsNotSelf.ToArray();
                break;
            case TargetTypes.Self:
                possibleTargets = new Entity[1];
                possibleTargets[0] = _entityManager.ActiveEntity;
                break;
            default:
                possibleTargets = _entityManager.Enemies.ToArray();
                break;
        }
        _buttonController.ChooseTarget(_entityManager.ActiveEntity);
        _targetChooser.StartChoose(possibleTargets);
    }
    
    private void DoAction(Entity actor, Entity target, FightCommandTypes type) {
        FightCommand fc = (FightCommand)_factory.GetCommand(type, actor, target);
        Invoker.AddCommand(fc);
        OnExecuteCombatCommand?.Invoke(actor.name, target.name, fc.Name);
        _entityManager.SetNextEntity();
    }

    private void Undo() {
        if (Invoker.CanUndo()){
            Invoker.Undo();
            OnUndoCombatCommand?.Invoke();
            _entityManager.SetPreviousEntity();
        }
    }

    internal void TargetChosen(ISelectable entity) {
        if(!(entity is Entity)) {
            Debug.LogError("Selected is not entity");
            return;
        }
        DoAction(_entityManager.ActiveEntity, (Entity) entity, _currentFCT);
    }
}