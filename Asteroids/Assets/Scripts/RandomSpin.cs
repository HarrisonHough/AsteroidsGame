using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Random Spin Class
*/

/// <summary>
/// 
/// </summary>
public class RandomSpin : MonoBehaviour {

    [SerializeField]
    private Transform _rotateTarget;    
    [SerializeField]
    private float _rotationSpeed = 3;

    private bool _rotate = false;
    // Use this for initialization
    private void Start () {
        if (_rotateTarget != null)
        {
            _rotate = true;
            StartCoroutine(Spin());
        }
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Quaternion RandomRotation()
    {
        float xRot = Random.Range(0, 360);
        float yRot = Random.Range(0, 360);
        float zRot = Random.Range(0, 360);
        //Debug.Log("Random rotation = " + xRot + ", " + yRot + ", " + zRot);

        return Quaternion.Euler(xRot, yRot, zRot);
    }    

    /// <summary>
    /// 
    /// </summary>
    private void OnDestroy() {
        _rotate = false;
        StopAllCoroutines();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Spin()
    {
        _rotateTarget.rotation = RandomRotation();
        while (_rotate)
        {
            _rotateTarget.Rotate(_rotateTarget.forward * Time.deltaTime * _rotationSpeed * 50);
            yield return null;
        }
    }

}
