using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReflectionFactory {
    public class EX6Manager : MonoBehaviour {

        [SerializeField]
        private GameObject _buttonPrefabb;
        [SerializeField]
        private GameObject _container;

        private List<GameObject> _buttonsIns;

        AnimalFactory _factory;

        void Start(){
            Clear();
            _factory = new AnimalFactory();
        }

        private void Clear(){
            if (_buttonsIns == null) _buttonsIns = new List<GameObject>();
            foreach(GameObject item in _buttonsIns){
                Destroy(item);
            }
            _buttonsIns.Clear();
        }

        private void InstanceButton(string name){
            GameObject btn = Instantiate(_buttonPrefabb);
            btn.transform.SetParent(_container.transform);
            btn.GetComponentInChildren<Text>().text = name;
            _buttonsIns.Add(btn);
        }

        public void ShowAllAnimals(){
            Clear();

            foreach (string str in _factory.GetNames()){
                Animals an = _factory.GetAnimal(str);
                InstanceButton(an.Name);
            }

        }

        public void ShowFlyingAnimals(){
            Clear();

            foreach (string str in _factory.GetNames()){
                Animals an = _factory.GetAnimal(str);
                
                if (an.Fly) InstanceButton(an.Name);
            }
        }

        public void ShowNonFlyingAnimals(){
            Clear();

            foreach (string str in _factory.GetNames()){
                Animals an = _factory.GetAnimal(str);
                
                if (!an.Fly) InstanceButton(an.Name);
            }
        }

    }
}