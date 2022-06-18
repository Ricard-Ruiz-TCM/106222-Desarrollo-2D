using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectBlock : Enemy
{
    public LayerMask WhatIsVisible;

    public bool NotBlocked;

    protected void CheckBlock(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, DetectionRange, WhatIsPlayer);
        if (hit){
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.right, DetectionRange, WhatIsVisible);
            NotBlocked = (hit2.collider.transform == Player.transform);
        } else {
            NotBlocked = false;
        }
    }
    
}
