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

    private Item item;

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
        
        //TODO move to more appropriate class
        switch (item.Type) {
            case ItemType.Shield:
                //TurnOnShield();
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
}
