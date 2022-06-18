using System;
using UnityEngine;
using System.Collections.Generic;

namespace ReflectionFactory {
    public class CombatCommandsUI : MonoBehaviour {

        [SerializeField]
        private GameObject _textLinePrefab;
        private List<GameObject> _textLines;

        // Variable para saber si el turno acabo
        private bool _turnEnded;

        void OnEnable(){
            EntityManager.OnEndTurn += EndTurn;
            CombatManager.OnUndoCombatCommand += RemoveCombatCommand;
            CombatManager.OnExecuteCombatCommand += AddCombatCommand;
        }

        void OnDisable(){
            EntityManager.OnEndTurn -= EndTurn;
            CombatManager.OnUndoCombatCommand -= RemoveCombatCommand;
            CombatManager.OnExecuteCombatCommand -= AddCombatCommand;
        }
        
        void Awake(){
            _turnEnded = false;
        }

        void Start(){
            Clear();
        }

        // Método para añadir una nueva linea de feedback sobre como se ejecuto una acción del combate
        public void AddCombatCommand(string executor, string target, string action){
            if (_turnEnded) StartTurn();
            InstantiateLine(executor + " " + action + " -> " + target);
        }

        public void RemoveCombatCommand(){
            if (_textLines.Count > 2){
                GameObject.Destroy(_textLines[_textLines.Count - 1]);
                _textLines.RemoveAt(_textLines.Count - 1);
            }
        }

        private void InstantiateLine(string text){

            if (_textLines == null) _textLines = new List<GameObject>();

            GameObject textLine = Instantiate(_textLinePrefab);
            textLine.transform.SetParent(this.transform);
            textLine.GetComponent<CombatCommandTextUI>().SetText(text);
            _textLines.Add(textLine.gameObject);
        }

        private void Clear() {

            if (_textLines == null) return;

            foreach (GameObject item in _textLines) {
                if(item) GameObject.Destroy(item);
            }
            _textLines.Clear();

        }

        public void StartTurn(){
            _turnEnded = false;
            Clear();
        }

        public void EndTurn(){
            _turnEnded = true;
        }

    }
}