using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Constant Velocity Class
*/

/// <summary>
/// SpawnPoint is a simple class that applies a constant speed
/// to an Object with a Rigidbody. This is used for asteroids
/// and missiles
/// </summary>
/// 
public class ConstantVelocity : MonoBehaviour {
       
    public float constantSpeed = 10f;

    private Rigidbody _rb;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	/// <summary>
    /// Called every fixed frame
    /// </summary>
	void FixedUpdate () {
        //applied within FixedUpdate for accurate physics
        _rb.velocity = transform.forward * constantSpeed;
	}
}
