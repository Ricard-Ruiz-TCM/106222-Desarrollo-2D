using UnityEngine;

public class ShieldCommand : FightCommand {

    // Nombre del comando
    public override string Name => "ShieldCommand";

    public int Defense => 5;

    // Constructor pasando el ejecutor y asignando tarjets
    public ShieldCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.FriendNotSelf; }

    public override void Excecute() {
        ((Fighter)_target).AddDefense(Defense);
    }

    public override void Undo() {
        ((Fighter)_target).AddDefense(-Defense);
    }

}
