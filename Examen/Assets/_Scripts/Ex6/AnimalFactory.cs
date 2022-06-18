using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ReflectionFactory {
    public class AnimalFactory {

        public Dictionary<string, Type> _animalsByName;

        public AnimalFactory() {

            var animalTypes = Assembly.GetAssembly(typeof(Animals)).GetTypes()
            .Where(x => !x.IsInterface && !x.IsAbstract && typeof(Animals).IsAssignableFrom(x));

            _animalsByName = new Dictionary<string, Type>();

            foreach (var type in animalTypes) {
                var tempCommand = Activator.CreateInstance(type);      
                _animalsByName.Add(type.Name, type);
            }
        }

        public Animals GetAnimal(string name) {
            if (_animalsByName.ContainsKey(name)) {
                return  Activator.CreateInstance(_animalsByName[name]) as Animals;
            }
            return null;
        }

        public string[] GetNames() {
            return _animalsByName.Keys.ToArray();
        }
        
    }
}