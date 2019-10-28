using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private float shieldDuration;

    private bool isSecondaryFire = false;
    // Start is called before the first frame update
    void Awake()
    {
        shield.SetActive(false);
        if(isSecondaryFire)
            InputController.OnSecondaryFireAction += Activate;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Activate() {

        StartCoroutine(ShieldOn());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator ShieldOn() 
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldDuration);

        //TODO add warning for when shield about to run out
        shield.SetActive(false);
    }
    
    
}
