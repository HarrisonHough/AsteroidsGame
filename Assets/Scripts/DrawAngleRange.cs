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

    
    private SpawnPoint _spawnPoint;
    private GameObject _left;
    private GameObject _right;

    /// <summary>
    /// Used for Initialization
    /// </summary>
	private void Start () {
        //get reference
        _spawnPoint = GetComponent<SpawnPoint>();

        //create transform for Left max angle
        _left = new GameObject("Left");
        _left.transform.position = transform.position;
        _left.transform.rotation = transform.rotation;
        _left.transform.Rotate(0, _spawnPoint.MaxYRotation, 0);
        _left.transform.parent = transform;

        //create transform for right max angle
        _right = new GameObject("Right");
        _right.transform.position = transform.position;
        _right.transform.rotation = transform.rotation;
        _right.transform.Rotate(0, -_spawnPoint.MaxYRotation, 0);
        _right.transform.parent = transform;
	}


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {

        //draw max _left ray
        Vector3 forward = _left.transform.forward * 10;
        Debug.DrawRay(transform.position, forward, Color.green, 2, false);

        //draw max _right ray
        forward = _right.transform.forward * 10;
        Debug.DrawRay(transform.position, forward, Color.green, 2, false);
    }
}
