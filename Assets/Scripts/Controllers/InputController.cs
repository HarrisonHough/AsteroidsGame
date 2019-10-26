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

    [SerializeField]
    private MissileLauncher missileControl;
    [SerializeField]
    private ParticleSystem thrustParticles;

    public delegate void MoveUp();
    public static event MoveUp OnMoveUp;

    public static Action OnPrimaryFireAction;
    public static Action OnSecondaryFireAction;
    

    protected Player player;

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
        OnPrimaryFireAction += missileControl.ShootMissile;
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
        
//TODO cleanup
//        if (Input.GetAxis("Horizontal") != 0)
//        {
//            player.Rotate(Input.GetAxis("Horizontal"));
//            
//        }
//        if (Input.GetAxis("Vertical") > 0)
//        {
//            OnMoveUp();            
//            
//        }
//        else
//            {
//                player.Slow();
//                thrustParticles.Stop();
//            }

        if (Input.GetButtonDown("Shoot")) {

            OnPrimaryFireAction.Invoke();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Thrust() {
        
        player.Thrust(Input.GetAxis("Vertical"));
        if (!thrustParticles.isPlaying)
            thrustParticles.Play();
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
