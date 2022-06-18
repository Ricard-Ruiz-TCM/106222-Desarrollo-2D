using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator _animator;
    private PlayerMovement _movement;

    void Awake(){
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update(){
        _animator.SetBool("Walk", _movement.IsMoving);
        _animator.SetBool("Sneak", _movement.IsSneak);
    }

}
