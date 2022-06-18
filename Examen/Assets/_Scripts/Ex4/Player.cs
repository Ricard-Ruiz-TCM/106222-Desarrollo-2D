using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    SpriteRenderer _image;
  
    //TODO subscribe to events and make the methods that set the text

    void OnEnable(){
        TriggerArea.OnEnter2Color += (Color c) => { _image.color = c; };
        TriggerArea.OnExit2Color += () => { _image.color = Color.white; };
    }

    void OnDisable(){
        TriggerArea.OnEnter2Color -= (Color c) => { _image.color = c; };
        TriggerArea.OnExit2Color -= () => { _image.color = Color.white; };
    }

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<SpriteRenderer>();
    }

  
}
