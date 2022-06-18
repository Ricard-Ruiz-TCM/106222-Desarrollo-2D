using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyJumper : MonoBehaviour
{
    //public float JumpForce;
    private Rigidbody2D _rigidbody;

    public float JumpHeight;
    public float TimeToMaxHeight;   

    private CollisionDetection collisionDetection;

  
    [SerializeField]
    private bool _jetPack;
    [SerializeField]
    private bool _fly;

    private float _maxFlyTime => 3.0f;
    private float _currentFlyTime;

    private float _lastVelocity_Y;

    public float PressTimeToMaxJump;

    private float _jumpStartedTime;

    public Text flytext;
   
    [SerializeField] 
    private bool _inGravityArea;

    [SerializeField] 
    private bool _canChangeGravity;
    
    [SerializeField] 
    public bool Touching;
    
    [SerializeField] 
    private bool _normalGravity;

    void OnEnable(){
        CollisionDetection.OnTouching += (bool touch) => { Touching = touch; };
    }

    void OnDisable(){
        CollisionDetection.OnTouching -= (bool touch) => { Touching = touch; };
    }
   
    // Start is called before the first frame update
    void Start()
    {
        _jetPack = false;
        _currentFlyTime = 3.0f;
        _normalGravity = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (_fly && _jetPack){
            _currentFlyTime -= Time.deltaTime;
            if (_currentFlyTime >= 0.25f){
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y + 0.25f);
            }
        } else {
            _currentFlyTime = Math.Max(0.0f, Math.Min(_currentFlyTime + Time.deltaTime, _maxFlyTime));
        }

        flytext.text = _currentFlyTime.ToString();

        if (Touching) _canChangeGravity = true;

        if (_canChangeGravity && _inGravityArea && PeakReached()) //Check if in area an peak reached here
            TweakGravity();
    }


    private void TweakGravity()
    {
       _normalGravity = !_normalGravity;
       _rigidbody.gravityScale = -_rigidbody.gravityScale;
       _canChangeGravity = false;
    }

    private bool PeakReached()
    {
        bool reached= ((_lastVelocity_Y * _rigidbody.velocity.y) < 0);
        _lastVelocity_Y = _rigidbody.velocity.y;
        return reached;
    }

    private float GetJumpForce()
    {
        //var vel = (_normalGravity ? 1 : -1) * 2 * JumpHeight / TimeToMaxHeight;      
        return  8.0f;
    }

    private void SetGravity()
    {
        var grav = (_normalGravity ? 1 : -1) * 2 * JumpHeight / (TimeToMaxHeight * TimeToMaxHeight);
        _rigidbody.gravityScale = grav / 9.81f;
    }

    public void OnJumpStarted()
    {
        _fly = true;
        if (!Touching)
            return;
        if (_jetPack)
            return;
            // SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
        _jumpStartedTime = Time.time;
    }

    public void OnJumpFinished()
    {
        _fly = false;
        float fractionOfTimePressed =  1 / 
            Mathf.Clamp01((Time.time - _jumpStartedTime) /
            PressTimeToMaxJump);
        //_rigidbody.gravityScale *= fractionOfTimePressed;
    }

    private void OnActiveJetpack(){
        _jetPack = !_jetPack;
    }
  

    internal void OnEnterGravityArea()
    {
       _inGravityArea = true;
    }

    internal void OnExitGravityArea()
    {
       _inGravityArea = false;
    }

}
