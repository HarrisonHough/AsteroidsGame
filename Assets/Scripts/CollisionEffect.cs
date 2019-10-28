using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Collision Effect Class
*/

/// <summary>
/// Collision Effect class is used to spawn an effect
/// (particles) on collision with particular objects.
/// </summary>
public class CollisionEffect : MonoBehaviour {

    #region Public Variables
    //the effect to spawn on collision
    [SerializeField]
    private GameObject particleEffect;
    [SerializeField]
    private bool destroyOnCollision;

    //the tag to check for on object collision
    [SerializeField]
    private string tagName;
    #endregion

    #region Functions
    /// <summary>
    /// Used for Initialization
    /// </summary>
    private void Start() {
        //check for null reference
        if (tagName == null)
            Debug.Log("TagName is not assigned");
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains(tagName)) {
            CreateParticles();
            if(destroyOnCollision)
                Destroy(gameObject);
        }
    }


    /// <summary>
    /// Called on collision, triggers CreateParticles function
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {

        //check for tag
        if (other.tag.Contains(tagName)) {
            CreateParticles();
            if(destroyOnCollision)
                Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates particles at current position (+ offset)
    /// </summary>
    private void CreateParticles() {
        //spawn particles just above object to make more visible from top view
        GameObject particles = Instantiate(particleEffect, transform.position + Vector3.up, transform.rotation);
    }
    #endregion
}
