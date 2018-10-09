using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testClimb : MonoBehaviour {

    //float timerStart = 10f;
    //float timer = 10f;

    // Update is called once per frame
    void Update()
    {
        // test rotation
        if (Input.GetKey("q"))
        {
            transform.Rotate(new Vector3(0, 0, 2));
        }
        else if(Input.GetKey("e"))
        {
            transform.Rotate(new Vector3(0, 0, -2));
        }

        if (Input.GetKey("a"))
        {
            GetLadderTask().GetComponent<LadderTask>().LeftHandUp();
        }
        else if (Input.GetKey("d"))
        {
            GetLadderTask().GetComponent<LadderTask>().RightHandUp();
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

    /* Find the ladder task object in order to communicate with commands to move/position legs and hands */
    public GameObject GetLadderTask()
    {
        return GameObject.Find("Ladder Task");
    }
}
