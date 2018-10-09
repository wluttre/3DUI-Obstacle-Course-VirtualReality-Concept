using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWandCollision : MonoBehaviour {
    //float timerStart = 10f;
    //float timer = 10f;

    // Update is called once per frame
    void Update()
    {

        // Move left hand
        if (Input.GetKey("i"))
        {
            transform.Find("Controller (left)").Translate(new Vector3(0, .01f, 0));
        }
        else if (Input.GetKey("k"))
        {
            transform.Find("Controller (left)").Translate(new Vector3(0, -.01f, 0));
        }

        // Move right hand
        if (Input.GetKey("o"))
        {
            transform.Find("Controller (right)").Translate(new Vector3(0,.01f,0));
        }
        else if (Input.GetKey("l"))
        {
            transform.Find("Controller (right)").Translate(new Vector3(0, -.01f, 0));
        }

        /*
        if (timer > timerStart/2)
        {
            timer -= Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 1));
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, -1));
        }
        else
            timer = timerStart;
        */
    }
}
