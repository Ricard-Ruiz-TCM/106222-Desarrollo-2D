using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CommandFactory {

    public Dictionary<FightCommandTypes, Type> _commandsByName;

    // Constructor
    public CommandFactory() {

        // Recuperamos todos los cripts que no sean abstractos, no sean interfaces y pueda asignase en ellos la interfaz "ICommand"
        var commandTypes = Assembly.GetAssembly(typeof(ICommand)).GetTypes()
            .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ICommand).IsAssignableFrom(x));

        _commandsByName = new Dictionary<FightCommandTypes, Type>();

        // Rellenamos el diccionario de referencia, donde asignamos
        // Nombre del comando al tipo de instancia que podremos crear
        int i = 0;
        object[] entities = new object[2]; entities[0] = entities[1] = null;
        foreach (var type in commandTypes) {
            var tempCommand = Activator.CreateInstance(type, entities);      
            _commandsByName.Add(((FightCommandTypes)i), type);
            i++;
        }
    }

    // Creador de comandos, según el nombre del comando asignado en el diccionario previamente
    public ICommand GetCommand(FightCommandTypes commandName, Entity executor = null, Entity target = null) {
        // Cargamos las dos entidades para crear el command pasandolas por parametro
        object[] entities = new object[2]; entities[0] = executor; entities[1] = target;
        if (_commandsByName.ContainsKey(commandName)) {
            return  Activator.CreateInstance(_commandsByName[commandName], entities) as ICommand;
        }
        return null;
    }

    // Método para recuperar todos los objetos posibles de creación del factory
    public FightCommandTypes[] GetNames() {
        return _commandsByName.Keys.ToArray();
    }
    
}