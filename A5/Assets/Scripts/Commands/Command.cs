
public abstract class Command : ICommand {

    // Nombre del comando
    public abstract string Name {get; }

    // Entidad de referencia, ejecutor del comando
    protected Entity _executor;

    // Contructor vacio
    protected Command(){}

    // Constructor pasandole el ejecutor
    protected Command(Entity executor) {
        _executor = executor;
    }

    // Métodos abstractos
    public abstract void Excecute();
    public abstract void Undo();

}
