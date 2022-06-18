
public interface ICommand {

    // Nombre del comando
    public string Name { get; }

    void Excecute();
    void Undo();

}
