using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipMotor : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField]
    private float thrustPower = 50;
    [SerializeField]
    private float stopSpeed = 2;
    [SerializeField]
    private float maxSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }
    public void Thrust(float thrust)
    {
        rigidbody.AddForce(transform.forward * thrustPower);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }
    
    /// <summary>
    /// This function is called whenever there is NO thrust
    /// to slow down faster than the default.
    /// This is also called from InputController in (In FixedUpdate())
    /// </summary>
    public void Slow() {

        //slows down rigidbody (self)
        rigidbody.velocity  = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * stopSpeed);
    }
}
