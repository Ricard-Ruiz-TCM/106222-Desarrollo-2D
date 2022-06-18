
public abstract class FightCommand : Command {

    // Objetivos del comando
    public TargetTypes PossibleTargets;

    // Entidad de referencia para el objetivo
    protected Entity _target;

    // Constructor pasando el ejecutor, lo ejecuta el padre
    public FightCommand(Entity executor, Entity target) : base(executor) { 
        _target = target; 
    }
    
}
