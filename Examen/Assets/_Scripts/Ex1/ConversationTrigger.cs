using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public Conversation DialogueData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        CharacterData data = GetComponent<Character>().CharacterData;
        data.Mood = DialogueData.StartMood;
        DialogueManager.StartDialogue(DialogueData, gameObject, data);
    }
}
