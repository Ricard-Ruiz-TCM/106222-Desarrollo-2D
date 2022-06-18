using System;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsObs;
    public LayerMask WhatIsGround;
    public float Speed;

    public float DistanceDetection => 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EdgeDetected() || ObstacleDetected()){
            //Flip();
            ChangeDirection();
        }
            
        Move();
    }

    private bool ObstacleDetected(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(EdgedetectionPoint.position, DistanceDetection, WhatIsObs);
        return (colliders.Length > 0);
    }

    private bool EdgeDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(EdgedetectionPoint.position, DistanceDetection, WhatIsGround);
        return (colliders.Length > 0);
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private void ChangeDirection(){
        
        float z = transform.localEulerAngles.z;
        transform.Rotate(0.0f, 0.0f, UnityEngine.Random.Range(-180.0f, 180.0f));
    }

   
}
