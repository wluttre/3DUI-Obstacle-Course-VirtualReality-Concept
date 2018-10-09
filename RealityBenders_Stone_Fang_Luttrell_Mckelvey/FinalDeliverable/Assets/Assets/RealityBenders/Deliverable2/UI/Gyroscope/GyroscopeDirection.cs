using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeDirection : MonoBehaviour {
    /**
     * What this Script Does: 
     * Implements gyroscope by matching direction of arrow to the user's orientation
     * **/
    public Quadcopter chopper;

    // Use this for initialization
    void Start () {
        //quad = GameObject.Find("Camera (eye)"); // follow flying camera
    }
	
	// Update is called once per frame
	void Update () {

        transform.rotation = chopper.transform.rotation;
    }
}
