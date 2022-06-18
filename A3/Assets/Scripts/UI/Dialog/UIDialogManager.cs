using System;
using UnityEngine;

public class UIDialogManager : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    void OnEnable(){
        TradeNode.OnStartTrade += HideDialog;
        TradeNode.OnExitTrade += ShowDialog;
        
        QuestNode.OnStartQuest += HideDialog;
        QuestNode.OnPickRewards += ShowDialog;

        GameManager.GameReset += ShowDialog;
    }

    void OnDisable(){
        TradeNode.OnStartTrade -= HideDialog;
        TradeNode.OnExitTrade -= ShowDialog;

        QuestNode.OnStartQuest -= HideDialog;
        QuestNode.OnPickRewards -= ShowDialog;

        GameManager.GameReset -= ShowDialog;
    }

    void Awake(){
        _animator = GetComponent<Animator>();
    }

    void Start(){
        ShowDialog();
    }

    public void ShowDialog(){
        _animator.SetBool("dialog", true);
    }

    public void HideDialog(){
        _animator.SetBool("dialog", false);
    }

}