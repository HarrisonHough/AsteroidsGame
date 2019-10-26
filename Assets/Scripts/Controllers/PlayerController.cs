using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MissileLauncher missileLauncher;
    [SerializeField]
    private ParticleSystem thrustParticles;
    [SerializeField]
    private Player player;
    [SerializeField]
    private InputController inputController;

    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    private void FixedUpdate()
    {
            Thrust(inputController.yInput);
    }

    public void Thrust(float thrust)
    {
        rigidbody.AddForce(transform.forward * 10f);
    }
    
    /// <summary>
    /// This function is called whenever there is NO thrust
    /// to slow down faster than the default.
    /// This is also called from InputController in (In FixedUpdate())
    /// </summary>
    public void Slow() {

        //slows down rigidbody (self)
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * 2);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void TurnOnShield() {

        //StartCoroutine(ShieldOn());
    }
}
