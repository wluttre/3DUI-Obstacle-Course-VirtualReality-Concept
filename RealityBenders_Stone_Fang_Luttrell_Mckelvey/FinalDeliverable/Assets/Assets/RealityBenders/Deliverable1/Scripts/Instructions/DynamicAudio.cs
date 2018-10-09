using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAudio : MonoBehaviour
{

    public AudioSource environment;
    public LadderTask ladders;
    private int contact;

    // Use this for initialization
    void Start()
    {
        environment = GetComponent<AudioSource>();

        if (!environment.isPlaying)

        {
            environment.Play();
        }
        environment.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (ladders.ViveCameraRig.transform.hasChanged)
        {
            environment.volume = 0.5f;
        }
        */





    }
}