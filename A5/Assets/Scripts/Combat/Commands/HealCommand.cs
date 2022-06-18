using UnityEngine;

public class HealCommand : FightCommand {

    // Nombre del comando
    public override string Name => "HealCommand";

    public int Heal => 3;

    // Constructor pasando el ejecutor y asignando tarjets
    public HealCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.Friend; }

    public override void Excecute() {
        ((Fighter)_target).Heal(Heal);
    }

    public override void Undo() {
        ((Fighter)_target).TakeDamage(Heal);
    }

}
