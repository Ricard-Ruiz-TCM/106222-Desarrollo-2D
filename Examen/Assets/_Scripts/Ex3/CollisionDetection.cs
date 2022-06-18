using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsRoof;

    [SerializeField]
    private Transform GroundCheckPoint;

    [SerializeField]
    private Transform RoofCheckPoint;

    private float checkRadius = 0.15f;
    private bool _wasGrounded;

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

   
    [SerializeField]
    private bool _isTouchingRoof;
    public bool IsTouchingRoof { get { return _isTouchingRoof; } }

    [SerializeField]
    private float _distanceToGround;
    public float DistanceToGround { get { return _distanceToGround; } }

    public static event Action<bool> OnTouching;
  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, checkRadius);
        Gizmos.DrawWireSphere(RoofCheckPoint.position, checkRadius);
        Gizmos.color = Color.white;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        //CheckRoof();
        OnTouching?.Invoke(/*_isTouchingRoof || */_isGrounded);
    }

  

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position,
          checkRadius, WhatIsRoof);
        _isTouchingRoof = colliders.Length > 0;
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position,
           checkRadius, WhatIsGround);
        _isGrounded =  colliders.Length > 0;

      
    }


    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position,
            Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
      
    }
}
