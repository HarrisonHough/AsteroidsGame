using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
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
    private Rigidbody rigidbody;

    [SerializeField]
    private float worldWrapPositionOffset = 0.5f;
    private float maxPositionX;
    private float maxPositionZ;
    private const string COLLISION_TAG = "WorldBox";

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        maxPositionX = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 8)).x);
        maxPositionZ = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 8)).z);
    }

    /// <summary>
    /// Called when entering the world box
    /// Used for debug purposes
    /// </summary>
    /// <param name="other">Collider: Collider of the object trigger</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ON TRIGGER ENTER CALLED");
        if (other.tag.Contains(COLLISION_TAG))
            Debug.Log("Entered World");
    }

    /// <summary>
    /// called when leaving the world box
    /// Triggers CheckPosition function
    /// </summary>
    /// <param name="other">Collider: Collider of the object trigger</param>
    void OnTriggerExit(Collider other)
    {
        Debug.Log("ON TRIGGER EXIT CALLED");
        //Debug.Log("Exited World");
        //Check when leaving world box
        if (other.tag.Contains(COLLISION_TAG))
        {
            Debug.Log("EXITING WORLD BOX");
            CheckPositionAndWrap();
        }
    }

    /// <summary>
    /// Jumps / Moves player to opposite side of the screen
    /// causing "world wrap" effect
    /// </summary>
    void CheckPositionAndWrap()
    {
        //store this objects position
        Vector3 objectPosition = transform.position;

        //Check X position
        if (objectPosition.x > maxPositionX)
        {
            
            //mirror position on X axis and subtract offset to prevent spawning outside world box
            objectPosition.x = - Mathf.Abs(maxPositionX - worldWrapPositionOffset);
        }
        else if (objectPosition.x < -maxPositionX)
        {
            //mirror position on X axis and subtract offset to prevent spawning outside world box
            objectPosition.x = maxPositionX - worldWrapPositionOffset;
        }

        //Check Z position
        if (objectPosition.z > maxPositionZ)
        {
            //mirror position on Z axis and subtract offset to prevent spawning outside world box
            objectPosition.z = -Mathf.Abs(maxPositionZ - worldWrapPositionOffset);

        }
        else if (objectPosition.z < -maxPositionZ)
        {
            //mirror position on Z axis and subtract offset to prevent spawning outside world box
            objectPosition.z = maxPositionZ - worldWrapPositionOffset;
        }

        //Finally set new position (mirrored as if wrapped around the screen)
        SetNewPosition(objectPosition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    private void SetNewPosition(Vector3 position) {
        transform.position = position;
    }
}
