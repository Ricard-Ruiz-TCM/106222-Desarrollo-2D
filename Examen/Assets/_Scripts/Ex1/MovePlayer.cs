using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToNPC());
    }

    private IEnumerator MoveToNPC()
    {
        Vector2 start = transform.position;
        Vector2 end = new Vector2(4, 0);
        float duration = 2;
        for (float t = 0; t < duration; t+= Time.deltaTime)
        {
            transform.position = Vector2.Lerp(start, end, t / duration);
            yield return null;
        }
    }

}
