using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Draw Angle Range Class
*/

/// <summary>
/// The Draw Angle Range class is only used for debug / setup
/// purposes. It provides a way to visualize and adjust the 
/// angle range of each spawner. (The Angle Range at which the 
/// asteroids can spawn with)
/// </summary>
public class DrawAngleRange : MonoBehaviour {

    
    private SpawnPoint spawnPoint;
    private GameObject left;
    private GameObject right;

    /// <summary>
    /// Used for Initialization
    /// </summary>
	private void Start () {
        //get reference
        spawnPoint = GetComponent<SpawnPoint>();

        //create transform for Left max angle
        left = new GameObject("Left");
        left.transform.position = transform.position;
        left.transform.rotation = transform.rotation;
        left.transform.Rotate(0, spawnPoint.MaxYRotation, 0);
        left.transform.parent = transform;

        //create transform for right max angle
        right = new GameObject("Right");
        right.transform.position = transform.position;
        right.transform.rotation = transform.rotation;
        right.transform.Rotate(0, -spawnPoint.MaxYRotation, 0);
        right.transform.parent = transform;
	}


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {

        //draw max left ray
        Vector3 forward = left.transform.forward * 10;
        Debug.DrawRay(transform.position, forward, Color.green, 2, false);

        //draw max right ray
        forward = right.transform.forward * 10;
        Debug.DrawRay(transform.position, forward, Color.green, 2, false);
    }
}
