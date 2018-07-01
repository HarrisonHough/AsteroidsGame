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
    private Vector2 _speedRange;
    [SerializeField]
    private int _pointsForKilling;

    private ConstantVelocity _velocity;

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected virtual void Start()
    {
        _velocity = GetComponent<ConstantVelocity>();
        SetInitialVelocity();

        
    }

    /// <summary>
    /// Sets the initial velocity
    /// </summary>
    private void SetInitialVelocity() {
        float speed = Random.Range(_speedRange.x, _speedRange.y);
        _velocity.constantSpeed = speed;
    }

    
}
