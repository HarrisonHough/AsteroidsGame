using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldBox : MonoBehaviour {

    private void Start()
    {
        SetToScreenSize();
    }

    void SetToScreenSize()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 7));
        position.y = 0;
        transform.position = position;
        
        print("Center Position = " + position);
        print("position at max X Screen space = " + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,8)));
        float maxPositionX = Mathf.Abs( Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 8)).x);
        float maxPositionZ = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 8)).z;
        //float worldWidth3D = ( maxPositionX - transform.position.x);
        transform.localScale = new Vector3(maxPositionX*2,1 , maxPositionZ * 2);
        print("Collider Size " + transform.localScale);
    }



}
