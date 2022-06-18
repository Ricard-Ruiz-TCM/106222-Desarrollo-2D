
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour {

    // Única Instance de DialogManager para utilizarl como Singleton
    public static DialogManager Instance;
    
    public TextMeshProUGUI _speakerText;
    public TextMeshProUGUI _speechText;
    
    public GameObject _options;
    public GameObject _optionsPrefab;

    private Node _currentNode;

    private List<GameObject> _shownObjects;

    void OnEnable(){
        DialogButtonUI.OnOptionChosen += NextNode;
        TradeNode.OnExitTrade += NextNode;
        QuestNode.OnPickRewards += NextNode;

        GameManager.GameReset += NextNode;
    }

    void OnDisable(){
        DialogButtonUI.OnOptionChosen -= NextNode;
        TradeNode.OnExitTrade -= NextNode;
        QuestNode.OnPickRewards -= NextNode;

        GameManager.GameReset -= NextNode;
    }

    void Awake() {
        if (Instance == null) Instance = this;
    }

    void Start(){
    }

    // Método statico para inciar el sistema de dialogo.
    internal static void StartConversation(Node node){
        Instance.ISC(node);
    }

    // Nétodo para iniciar en el DialogManager un nodo en concreto
    // @param Node node -> Nodo actual para el DialogManager
    private void ISC(Node node){
        SetParent(node);
        SetCurrentNode(node);
        SetTexts();
        SetUpButtons();
    }

    // Método para establecer el _CurrentNode
    // @param Node node -> nuevo current node
    private void SetCurrentNode(Node node){
        _currentNode = node;
    }

    // Método para establecer el _parentNode
    // Al siguiente nodo al que me dirijo, le indico de donde venco por si acaso puedo volver
    // @param Node node -> Nodo destino (el nodo actual será el padre de este)
    private void SetParent(Node node){
        node._parentNode = _currentNode;
    }

    // Método para establecer los textos del locutor y escena del nodo
    private void SetTexts(){
         _speakerText.text = _currentNode._speakerText;
        _speechText.text = _currentNode._speechText;
    }

    // Método para instanciar los botones necesario para una correcta gestion del nodo para el flow
    private void SetUpButtons(){

        if (_shownObjects == null) _shownObjects = new List<GameObject>();

        // Destruimos todos los botones del nodo anterior
        foreach (var item in _shownObjects) {
            if(item) Destroy(item);
        }
        _shownObjects.Clear();

        // Instanciamos los botones nuevos
        for (int i = 0; i < _currentNode._options.Count; i++){
            SetButton(i, _currentNode._options[i]._text);
        }

        // Si el nodo actual permite volver al anterior, añadimos la opción con flow de -1
        if (_currentNode._canBack){
            SetButton(-1, "Volver");
        }

    }

    // Método para establecer la información del botoón
    // @param int id -> id del flow
    // @param string txt -> Texto para mostrar en el boton
    private void SetButton(int id, string txt){
        GameObject btn = Instantiate(_optionsPrefab);
        btn.transform.SetParent(_options.transform);
        btn.GetComponent<DialogButtonUI>().SetFlow(id);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = txt;
        _shownObjects.Add(btn);
    }

    // Override de NextNode para permitir una ejecución con un 0 de valor
    public void NextNode(){
        NextNode(0);
    }

    // Método para cambiar de nodo según el orden de la opción escogida
    // en el caso de recibir un -1, significa que queremos volver al nodo anterior
    // @param int i -> Posición del boton en las opciones del nodo, relacionado directamente con el siguiente nodo.
    public void NextNode(int i){
        Node next;
        if (i == -1) next = _currentNode._parentNode;
        else next = _currentNode._options[i]._nextNode;


        // Al cambiar de nodo, ejecutamos su action si tiene
        if (_currentNode is IExitNode) OnExitNode((IExitNode)_currentNode);

        // Al salir del nodo, ejcutamos su endnode
        if (next is IEnterNode) OnEnterNode((IEnterNode) next);

        // Cambiamos de nodo
        ISC(next);
        
    }


    // Método para la ejecución del EndNode
    // @param EndNode node -> objeto de EndNode
    public void OnExitNode(IExitNode node){
        node.OnExitNode();
    }

    // Método para la ejecución del enternode
    // @param ienternode node -> objeto de enter node
    public void OnEnterNode(IEnterNode node){
        node.OnEnterNode();
    }

}
