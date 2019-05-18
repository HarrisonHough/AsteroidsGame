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
    public delegate void ActionPress();
    public static event ActionPress OnActionPress;

    protected Player player;

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
        player = GetComponent<Player>();
        missileControl = GetComponent<MissileLauncher>();
        if (thrustParticles == null)
        {
            Debug.Log("Thrust particles not assigned");
        }
        OnActionPress += missileControl.ShootMissile;
        OnMoveUp += Thrust;
    }

    /// <summary>
    /// Called once per fixed frame
    /// Input is checked in here to ensure that physics are
    /// calculated correctly (player movement collisions)
    /// </summary>
    void FixedUpdate()
    {
        KeyboardInput();
    }

    /// <summary>
    /// Listens for Keyboard input
    /// Used for Keyboard control scheme
    /// </summary>
    void KeyboardInput()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            player.Rotate(Input.GetAxis("Horizontal"));
            
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            OnMoveUp();            
            
        }
        else
            {
                player.Slow();
                thrustParticles.Stop();
            }

        if (Input.GetButtonDown("Shoot")) {

            OnActionPress();
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
        OnActionPress -= missileControl.ShootMissile;
        OnMoveUp -= Thrust;
    }
}
