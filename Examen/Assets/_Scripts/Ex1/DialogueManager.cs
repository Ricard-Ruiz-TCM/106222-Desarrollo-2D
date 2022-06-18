using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Animator DialogueAnimator;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI SpeechText;
    public TextMeshProUGUI[] OptionsText;
    public Moodometer Moodometer;

    private DialogueNode _currentNode;
    private Conversation _currentConversation;

    private GameObject _talker;
    private CharacterData _talkerData;

   

    private void Awake()
    {
        Instance = this;
    }

    public void OptionChosen(int option)
    {

        _talkerData.Mood += _currentNode.Options[option].Mood;

        if (_talkerData.Mood < _currentConversation.MinMood){
            HideDialogue();
            _talker.GetComponent<Character>().BeAngry();
            return;
        }

        if (_talkerData.Mood > _currentConversation.MaxMood){
            HideDialogue();
            _talker.GetComponent<Character>().BeHappy();
            return;
        }

        SetMoodMeter();

        _currentNode = _currentNode.Options[option].NextNode;

        SetText(_currentNode);
      
    }

    private void SetMoodMeter(){
        float f = (float)(_talkerData.Mood - _currentConversation.MinMood) / (float)(_currentConversation.MaxMood - _currentConversation.MinMood);
        Moodometer.SetValue(f);
    }

   

    private void ShowDialogue()
    {
        DialogueAnimator.SetBool("Show", true);
    }

    private void HideDialogue()
    {
        DialogueAnimator.SetBool("Show", false);
    }

    internal static void StartDialogue(Conversation dialogueData, GameObject talker, CharacterData data)
    {
        
        Instance._StartDialogue(dialogueData, talker, data);
    }

    private void _StartDialogue(Conversation dialogueData, GameObject talker, CharacterData data)
    {
        _talker = talker;
        _talkerData = data;
        _currentConversation = dialogueData;
        _currentNode = dialogueData.StartNode;
       
        NameText.text = data.Name;
        SetText(_currentNode);

        ShowDialogue();

        SetMoodMeter();

    }

    private void SetText(DialogueNode node)
    {
        SpeechText.text = node.Text;
        for (int i = 0; i < OptionsText.Length; i++)
        {
            if(i < node.Options.Count)
            {
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                OptionsText[i].text = node.Options[i].Text;
            }
            else
            {
                OptionsText[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
