using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
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
       
    [SerializeField]
    private float constantSpeed = 10f;
    public float ConstantSpeed { get { return constantSpeed; } set { constantSpeed = value; } }

    private Rigidbody rigidbody;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	/// <summary>
    /// Called every fixed frame
    /// </summary>
	void FixedUpdate () {
        //applied within FixedUpdate for accurate physics
        rigidbody.velocity = transform.forward * ConstantSpeed;
	}
}
