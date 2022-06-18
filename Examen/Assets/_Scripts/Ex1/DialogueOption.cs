﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption 
{
    public string Text;  
    public DialogueNode NextNode;
    public int Mood;
}


