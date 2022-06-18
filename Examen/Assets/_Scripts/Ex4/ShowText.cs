using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private Text _text;

  
    //TODO subscribe to events and make the methods that set the text

    void OnEnable(){
        TriggerArea.OnEnter += (string text) => { _text.text = text; };
        TriggerArea.OnExit += () => { _text.text = ""; };
    }

    void OnDisable(){
        TriggerArea.OnEnter -= (string text) => { _text.text = text; };
        TriggerArea.OnExit -= () => { _text.text = ""; };
    }

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

  
}
