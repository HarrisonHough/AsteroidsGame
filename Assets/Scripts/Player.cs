using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Player Class
*/

/// <summary>
/// Player class controls the collision and movement events of the
/// player object
/// </summary>
/// 
public class Player : MonoBehaviour {
        
    [Range(0,5)][SerializeField]
    private float rotateSpeed = 1;
    [SerializeField]
    private float thrustPower = 50;
    [SerializeField]
    private float stopSpeed = 2;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private float shieldDuration;

    private Item item;
    private Rigidbody rb;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        Initialize();
	}

    /// <summary>
    /// 
    /// </summary>
    void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        shield.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            Death();
        }
    }

    /// <summary>
    /// Called on collision to trigger Death function
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy")
        {
            Death();
        }
        else if (other.tag == "Item") {
           item = other.gameObject.GetComponent<Item>();
            CheckItem();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckItem() {
        switch (item.Type) {
            case ItemType.Shield:
                TurnOnShield();
                break;
            case ItemType.Life:

                break;
            case ItemType.Bomb:

                break;
        }
        item.Destroy();
    }

    /// <summary>
    /// Called PlayerDeath function in GameManager before destroying
    /// self
    /// </summary>
    public void Death() {

        GameManager.Instance.PlayerDeath();
        gameObject.SetActive(false);
    }
	
    /// <summary>
    /// Rotates object over time, called from InputController class 
    /// (in FixedUpdate() for accurate physics)
    /// </summary>
    /// <param name="value"></param>
    public void Rotate(float value) {

        transform.Rotate(0, Time.deltaTime* value * (rotateSpeed * 100),0 );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void MouseLook(Vector3 target) {
        target.y = 0;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, rotateSpeed/20,0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }


    /// <summary>
    /// Moves player by adding force to rigidbody
    /// This is called from InputController class (In FixedUpdate() for accurate physics)
    /// </summary>
    /// <param name="value"></param>
    public void Thrust(float value) {

        //move object by adding force behind it
        rb.AddForce(transform.forward * thrustPower);
    }

    /// <summary>
    /// This function is called whenever there is NO thrust
    /// to slow down faster than the default.
    /// This is also called from InputController in (In FixedUpdate())
    /// </summary>
    public void Slow() {

        //slows down rigidbody (self)
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 2);
    }

    /// <summary>
    /// 
    /// </summary>
    public void TurnOnShield() {

        StartCoroutine(ShieldOn());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator ShieldOn() {

        shield.SetActive(true);
        yield return new WaitForSeconds(shieldDuration);

        //TODO add warning for when shield about to run out
        shield.SetActive(false);
    }
}
