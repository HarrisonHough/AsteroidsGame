using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Random Spin Class
*/

public class RandomSpin : MonoBehaviour {

    public Transform rotateTarget;    
    public float rotationSpeed = 3;

    private bool _rotate = false;
    // Use this for initialization
    private void Start () {
        if (rotateTarget != null)
        {
            _rotate = true;
            StartCoroutine(Spin());
        }
	}

    private Quaternion RandomRotation()
    {
        float xRot = Random.Range(0, 360);
        float yRot = Random.Range(0, 360);
        float zRot = Random.Range(0, 360);
        //Debug.Log("Random rotation = " + xRot + ", " + yRot + ", " + zRot);

        return Quaternion.Euler(xRot, yRot, zRot);
    }    

    private void OnDestroy() {
        _rotate = false;
        StopAllCoroutines();

    }

    IEnumerator Spin()
    {
        rotateTarget.rotation = RandomRotation();
        while (_rotate)
        {
            rotateTarget.Rotate(rotateTarget.forward * Time.deltaTime * rotationSpeed * 50);
            yield return null;
        }
    }

}
