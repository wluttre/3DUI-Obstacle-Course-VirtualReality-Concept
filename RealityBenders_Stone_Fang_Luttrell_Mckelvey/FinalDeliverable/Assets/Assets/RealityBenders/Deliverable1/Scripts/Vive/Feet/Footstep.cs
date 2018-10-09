using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    public float delay = 0.5f;
    
    public Boolean playAudio = false;
    public Boolean startDelay = false;
    public AudioClip audioFootOne, audioFootTwo, audioFootThree;
   
    public void PlayFootstep()
    {
        int randomAudio = UnityEngine.Random.Range(1, 3);

        startDelay = true;
        if (randomAudio == 1)
        {
            GetComponent<AudioSource>().clip = audioFootOne;
        }
        else if (randomAudio == 2) {
            GetComponent<AudioSource>().clip = audioFootTwo;

        }
        else if (randomAudio == 3) {
            GetComponent<AudioSource>().clip = audioFootThree;
        }

    }
    public void Update() {
        if( startDelay == true) {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                GetComponent<AudioSource>().Play();
                delay = 0.5f;
                startDelay = false;
            }
         }
    }
}

