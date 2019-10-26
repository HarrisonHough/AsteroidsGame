using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController), typeof(ShipMotor))]
public class PlayerMovement : MonoBehaviour
{
    [Range(0,5)][SerializeField]
    private float rotateSpeed = 1;
    [SerializeField]
    private ShipMotor shipMotor;
    private InputController inputController;
    [SerializeField]
    private ParticleSystem thrustParticles;
    // Start is called before the first frame update
    
    void Start()
    {
        inputController = GetComponent<InputController>();
        shipMotor = GetComponent<ShipMotor>();
        if(thrustParticles == null)
            thrustParticles = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate(inputController.xInput);

        if (inputController.yInput > 0)
        {
            shipMotor.Thrust(inputController.yInput);        
            if(!thrustParticles.isPlaying)
                thrustParticles.Play();
        }
        else
        {
            shipMotor.Slow();
            thrustParticles.Stop();
        }
    }
    
    /// <summary>
    /// Rotates object over time, called from InputController class 
    /// (in FixedUpdate() for accurate physics)
    /// </summary>
    /// <param name="value"></param>
    public void Rotate(float value) {

        transform.Rotate(0, Time.deltaTime* value * (rotateSpeed * 100),0 );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void MouseLook(Vector3 target) {
        target.y = 0;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, rotateSpeed/20,0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
