using UnityEngine;

public class BoostAttackCommand : FightCommand {

    // Nombre del comando
    public override string Name => "BoostAttackCommand";

    public int BoostAttack => 1;

    // Constructor pasando el ejecutor y asignando tarjets
    public BoostAttackCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.Self; }

    public override void Excecute() {
        ((Fighter)_target).AddAttackPermanent(BoostAttack);
    }

    public override void Undo() {
        ((Fighter)_target).AddAttackPermanent(-BoostAttack);
    }

}
