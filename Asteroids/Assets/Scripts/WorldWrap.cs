using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: World Wrap Class
*/

/// <summary>
/// World Wrap controls the "World Wrap" around effect so that
/// when objects leave the map, they appear on the other side with 
/// the same velocity
/// </summary>
public class WorldWrap : MonoBehaviour
{
    private Rigidbody _rb;
    private float _posXBound = 8.5f;
    private float _posZBound = 4.8f;
    private float _posXMax = 8.45f;
    private float _posZMax = 4.75f;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called when entering the world box
    /// Used for debug purposes
    /// </summary>
    /// <param name="other">Collider: Collider of the object trigger</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WorldBox")
            Debug.Log("Entered World");
    }

    /// <summary>
    /// called when leaving the world box
    /// Triggers CheckPosition function
    /// </summary>
    /// <param name="other">Collider: Collider of the object trigger</param>
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited World");
        if (other.tag == "WorldBox")
            CheckPosition();
    }

    /// <summary>
    /// Jumps / Moves player to opposite side of the screen
    /// causing "world wrap" effect
    /// </summary>
    void CheckPosition()
    {
        //Debug.Log("setting position");
        Vector3 pos = transform.position;
        //8.0003 4.503
        //check X position
        if (pos.x > _posXBound)
        {
            pos.x = -_posXMax;
        }
        else if (pos.x < -_posXBound)
        {
            pos.x = _posXMax;
            
        }
        else
        {
            pos.x = -pos.x;            
        }

        //check z position
        if (pos.z > _posZBound)
        {
            pos.z = -_posZMax;
        }
        else if (pos.z < -_posZBound)
        {
            pos.z = _posZMax;
        }
        else
        {
            pos.z = -pos.z;
        }

        SetNewPosition(pos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    private void SetNewPosition(Vector3 position) {
        transform.position = position;
    }
}
