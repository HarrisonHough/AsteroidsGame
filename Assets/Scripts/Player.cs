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
    private float _rotateSpeed = 1;
    [SerializeField]
    private float _thrustPower = 50;
    [SerializeField]
    private float _stopSpeed = 2;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _shieldDuration;

    private Item _item;
    private Rigidbody _rb;

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
        _rb = GetComponent<Rigidbody>();
        _shield.SetActive(false);
    }

    /// <summary>
    /// Called on collision to trigger Death function
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other) {


        if (other.tag == "Enemy")
        {
            Death();
        }
        else if (other.tag == "Item") {
           _item = other.gameObject.GetComponent<Item>();
            CheckItem();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckItem() {
        switch (_item.type) {
            case ItemType.Shield:
                TurnOnShield();
                break;
            case ItemType.Life:

                break;
            case ItemType.Bomb:

                break;
        }
        _item.Destroy();
    }

    /// <summary>
    /// Called PlayerDeath function in GameManager before destroying
    /// self
    /// </summary>
    public void Death() {

        GameManager.instance.PlayerDeath();
        gameObject.SetActive(false);
    }
	
    /// <summary>
    /// Rotates object over time, called from InputController class 
    /// (in FixedUpdate() for accurate physics)
    /// </summary>
    /// <param name="value"></param>
    public void Rotate(float value) {

        transform.Rotate(0, Time.deltaTime* value * (_rotateSpeed * 100),0 );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void MouseLook(Vector3 target) {
        target.y = 0;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, _rotateSpeed/20,0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }


    /// <summary>
    /// Moves player by adding force to rigidbody
    /// This is called from InputController class (In FixedUpdate() for accurate physics)
    /// </summary>
    /// <param name="value"></param>
    public void Thrust(float value) {

        //move object by adding force behind it
        _rb.AddForce(transform.forward * _thrustPower);
    }

    /// <summary>
    /// This function is called whenever there is NO thrust
    /// to slow down faster than the default.
    /// This is also called from InputController in (In FixedUpdate())
    /// </summary>
    public void Slow() {

        //slows down rigidbody (self)
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * 2);
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

        _shield.SetActive(true);
        yield return new WaitForSeconds(_shieldDuration);

        //TODO add warning for when shield about to run out
        _shield.SetActive(false);
    }
}
