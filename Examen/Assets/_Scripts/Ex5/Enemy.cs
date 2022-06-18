using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public LayerMask WhatIsPlayer;
   
    public float DetectionRange;
   
    public Transform Player;

    public bool InRange;
   
    protected void CheckRange(){
        InRange = Vector2.Distance(Player.position, transform.position) < DetectionRange;
    }

}
