using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData CharacterData;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = CharacterData.Sprite;
    }

    public void BeHappy()
    {
        GetComponent<Animator>().SetTrigger("SetHappy");
    }

    public void BeAngry()
    {
        GetComponent<Animator>().SetTrigger("SetAngry");
    }
}
