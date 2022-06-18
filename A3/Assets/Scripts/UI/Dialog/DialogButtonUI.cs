using System;
using UnityEngine;

public class DialogButtonUI : MonoBehaviour {

    [SerializeField]
    private int _pos;

    // Observer para gestionar el flow de los dialogos
    public static event Action<int> OnOptionChosen;

    // Método para establecer el flow de escenas
    // @param int pos -> posición de la opción del botoón en el dialogo
    public void SetFlow(int pos){
        _pos = pos;
    }

    // Método para ejecutar al clicar en el dialogo
    public void OnClick(){
        OnOptionChosen?.Invoke(_pos);
    }

}