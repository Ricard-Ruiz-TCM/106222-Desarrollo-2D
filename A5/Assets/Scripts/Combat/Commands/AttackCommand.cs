using UnityEngine;

public class AttackCommand : FightCommand {

    // Nombre del comando
    public override string Name => "Attack";

    private float Damage;
    
    // Constructor pasando el ejecutor y asignando tarjets
    public AttackCommand(Entity executor, Entity target) : base(executor, target) { PossibleTargets = TargetTypes.Enemy; }
    
    public override void Excecute() {
        Damage = ((Fighter)_executor).Attack;
        ((Fighter)_target).TakeDamage(Damage);
    }
    
    public override void Undo() {
        ((Fighter)_target).Heal(Damage);
    }

}
