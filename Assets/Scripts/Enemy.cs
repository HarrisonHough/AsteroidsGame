using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Enemy Class
*/


public class Enemy : MonoBehaviour {
       
    [SerializeField]
    private Vector2 speedRange;
    [SerializeField]
    private int pointsForKilling;

    private ConstantVelocity velocity;

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected virtual void Start()
    {
        velocity = GetComponent<ConstantVelocity>();
        SetInitialVelocity();

        
    }

    /// <summary>
    /// Sets the initial velocity
    /// </summary>
    private void SetInitialVelocity() {
        float speed = Random.Range(speedRange.x, speedRange.y);
        velocity.ConstantSpeed = speed;
    }

    
}
