using UnityEngine;

public class BoostDefenseCommand : FightCommand {

    // Nombre del comando
    public override string Name => "BoostDefenseCommand";

    public int BoostDefense => 1;

    // Constructor pasando el ejecutor y asignando tarjets
    public BoostDefenseCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.Self; }

    public override void Excecute() {
        ((Fighter)_target).AddDefensePermanent(BoostDefense);
    }

    public override void Undo() {
        ((Fighter)_target).AddDefensePermanent(-BoostDefense);
    }

}
