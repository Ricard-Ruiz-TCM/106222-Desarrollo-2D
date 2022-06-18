using UnityEngine;

public class DanceCommand : FightCommand {

    // Nombre del comando
    public override string Name => "Dance";

    private float Amount => 1;
    
    // Constructor pasando el ejecutor y asignando tarjets
    public DanceCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.Self; }
    
    public override void Excecute() {
        ((Fighter)_target).Heal(Amount);
    }
    
    public override void Undo() {
        ((Fighter)_target).TakeDamage(Amount);
    }

}
