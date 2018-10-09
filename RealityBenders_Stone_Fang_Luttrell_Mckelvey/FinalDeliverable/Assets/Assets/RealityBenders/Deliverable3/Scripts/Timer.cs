using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    /* Timer based on when the user has loaded into the scene/level */

    private int currentTime, startingTime;

	void Start () {
        startingTime = (int)Time.time;
	}
	
	void Update () {
        currentTime = (int)Time.time - startingTime;
	}

    public int GetCurrentTime()
    {
        return currentTime;
    }
}
