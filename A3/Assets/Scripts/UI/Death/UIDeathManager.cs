using UnityEngine;

public class UIDeathManager : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    void OnEnable(){
        DeathNode.OnDie += ShowDeath;
        PlayerStats.OnDie += ShowDeath;

        GameManager.GameReset += HideDeath;
    }

    void OnDisable(){
        DeathNode.OnDie -= ShowDeath;
        PlayerStats.OnDie -= ShowDeath;

        GameManager.GameReset -= HideDeath;
    }

    void Awake(){
        _animator = GetComponent<Animator>();
    }

    void Start(){
        HideDeath();
    }

    public void ShowDeath(){
        _animator.SetBool("death", true);
    }

    public void HideDeath(){
        _animator.SetBool("death", false);
    }

}
