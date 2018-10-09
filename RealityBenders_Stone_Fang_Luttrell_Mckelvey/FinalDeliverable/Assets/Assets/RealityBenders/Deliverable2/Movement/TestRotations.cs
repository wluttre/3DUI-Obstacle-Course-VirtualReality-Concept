using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotations : MonoBehaviour
{

    //float timerStart = 10f;
    //float timer = 10f;

    // Update is called once per frame
    void Update()
    {
        // test rotation
        if (Input.GetKey("q"))
        {
            transform.Rotate(new Vector3(0, 0, 1));
        }
        else if (Input.GetKey("e"))
        {
            transform.Rotate(new Vector3(0, 0, -1));
        }
        else if (Input.GetKey("w"))
        {
            transform.Rotate(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKey("s"))
        {
            transform.Rotate(new Vector3(1, 0, 0));
        }
        else if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, -1, 0));
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }

    }

    /* Find the ladder task object in order to communicate with commands to move/position legs and hands */
    public GameObject GetLadderTask()
    {
        return GameObject.Find("Ladder Task");
    }
}
