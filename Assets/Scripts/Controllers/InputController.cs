using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Input Controller Class
*/

/// <summary>
/// Input Controller class handles all of the user Input 
/// (other than UI Events)
/// </summary>
public class InputController : MonoBehaviour {

    public static Action OnPrimaryFireAction;
    public static Action OnSecondaryFireAction;
    public static Action OnYInputEndAction;

    private bool NoInputY = false;
    
    public float xInput
    {
        get;
        private set;
    }
    public float yInput
    {
        get;
        private set;
    }

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

    /// <summary>
    /// Called once per fixed frame
    /// Input is checked in here to ensure that physics are
    /// calculated correctly (player movement collisions)
    /// </summary>
    void Update()
    {
        KeyboardInput();
    }

    /// <summary>
    /// Listens for Keyboard input
    /// Used for Keyboard control scheme
    /// </summary>
    void KeyboardInput()
    {
        
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        if(!NoInputY && yInput < 0.05f)
            OnYInputEndAction?.Invoke();
        
        if (Input.GetButtonDown("Shoot")) {

            OnPrimaryFireAction?.Invoke();
        }

        NoInputY = yInput < 0.05f;
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDestroy() {
        //must unsubscribe before destroying
        OnPrimaryFireAction = null;
        OnSecondaryFireAction = null;
    }
}
