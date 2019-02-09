using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Item Class
*/

public enum ItemType { Shield, Bomb, Life };

public class Item : MonoBehaviour {

    
    public ItemType Type;
    private ConstantVelocity _velocity;
    public Vector2 SpeedRange;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        _velocity = GetComponent<ConstantVelocity>();
        float speed = Random.Range(SpeedRange.x, SpeedRange.y);
        _velocity.ConstantSpeed = speed;

    }


    public void Destroy() {
        Destroy(gameObject);
    }


}
