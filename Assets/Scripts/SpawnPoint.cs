using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Spawn Point Class
*/

/// <summary>
/// SpawnPoint is a simple class that stores a float value
/// used to determine the range of Y rotation values asteroids 
/// can spawn in with
/// </summary>
/// 
public class SpawnPoint : MonoBehaviour {

    //max Y roatation is used to determine the range of Y Rotation asteroids can
    //spawn in with. eg maxYRotation = 40 the range is between -40 and 40
    //Make this public so that I can adjust it per spawn point in inspector as 
    //each point will need to be adjusted according to their position
    public float MaxYRotation;


}
