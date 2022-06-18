using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInverterArea : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var jumper = collision.transform.GetComponent<MyJumper>();
        if (jumper != null)
        {
            jumper.OnEnterGravityArea();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var jumper = collision.transform.GetComponent<MyJumper>();
        if (jumper != null)
        {
            jumper.OnExitGravityArea();
        }
    }
}
