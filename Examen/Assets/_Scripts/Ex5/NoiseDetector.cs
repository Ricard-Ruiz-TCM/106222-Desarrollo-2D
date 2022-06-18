using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDetector : EnemyDetectBlock
{
    void Update(){
        CheckRange();
        if (InRange){
            CheckBlock();
        }
    }
    
}
