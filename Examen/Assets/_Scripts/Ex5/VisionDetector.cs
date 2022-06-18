using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : EnemyDetectBlock
{
    public float VisionAngle;

    public bool InFOV;

    void Update(){
        CheckRange();
        if (InRange){
            CheckBlock();
            if (NotBlocked){
                InFOV = EntityInPOV(Player.transform);
            }
        }
    }

    public bool EntityInPOV(Transform entity){
        Vector2 v1 = transform.right;
        Vector2 v2 = entity.position - transform.position;
        float angle = Vector2.Angle(v1, v2);
        return (VisionAngle >= 2*angle);
    }


}
