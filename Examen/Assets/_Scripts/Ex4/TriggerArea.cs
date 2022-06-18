using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public string ColorName;

    public static event Action<Color> OnEnter2Color;
    public static event Action OnExit2Color;

    public static event Action<string> OnEnter;
    public static event Action OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            OnEnter?.Invoke(ColorName);
            OnEnter2Color?.Invoke(GetComponent<SpriteRenderer>().color);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            OnExit?.Invoke();
            OnExit2Color?.Invoke();
        }
    }
}
