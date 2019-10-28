using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Sound Controller Class
*/

public class SoundController : MonoBehaviour {

    public AudioClip[] Audio;
    private AudioSource source;
    /*
        Audio array order
        0 - Button
        1 - collision 1
        2 - collision 2
        3 - explode 1
        4 - explode 2
        5 - shoot
    */

    /// <summary>
    /// 
    /// </summary>
    private void Start() {
       source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play collision sound
    /// </summary>
    public void Collision() {
        int i = Random.Range(1,2);
       PlaySound(i);
    }

    /// <summary>
    /// Play button press sound
    /// </summary>
    public void ButtonPress() {
       PlaySound(0);
    }

    /// <summary>
    /// Play player shoot sound
    /// </summary>
    public void PlayerShoot() {
        PlaySound(5);
    }

    /// <summary>
    /// Play Player explode sound
    /// </summary>
    public void PlayerExplode() {
        int i = Random.Range(3,4);
        PlaySound(i);
    }

    public void PlaySound(int i)
    {
        Debug.Log($"PLAY AUDIO CALLED CLIP ARRAY LENGTH = {Audio.Length} INDEX = {i}");
        if (Audio.Length > i)
        {
            Debug.Log("INDEX VALID PLAY AUDIO");
            source.PlayOneShot(Audio[i]);
        }
    }
}
