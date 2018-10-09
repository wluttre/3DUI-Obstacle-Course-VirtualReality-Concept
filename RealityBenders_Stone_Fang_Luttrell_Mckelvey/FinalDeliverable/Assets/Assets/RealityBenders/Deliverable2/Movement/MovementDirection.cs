using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDirection : MonoBehaviour {
    /**
     * What this Script Does: 
     * Implements flying direction based on the direction of the controllers.
     * Implements a max turning speed so the user can not do sharp turns that are disorienting.
     * Implements the deadzone of how far the controllers must be placed in order to start turning. 
     * **/
    private float power;
    public float maxTurnSpeed;
    public float deadZone; //degrees
    private GameObject eyes;
    public Quadcopter chopper;

    // Use this for initialization
    void Start () {
        eyes = GameObject.Find("Camera (eye)"); // eyes position/head
        eyes.transform.rotation = transform.rotation;
        //GetComponent<Quadcopter>().gyroStabilization = true;
    }

    // Update is called once per frame
    void Update() {

        eyes.transform.position = transform.position;

        float quadX = transform.eulerAngles.x;
        float quadY = transform.eulerAngles.y;
        float quadZ = transform.eulerAngles.z;

        float eyesX = eyes.transform.eulerAngles.x;
        float eyesY = eyes.transform.eulerAngles.y;
        float eyesZ = eyes.transform.eulerAngles.z;

        float differenceX = Math.Abs(eyesX - quadX);
        float differenceY = Math.Abs(eyesY - quadY);
        float differenceZ = Math.Abs(eyesZ - quadZ);

        float rotationSpdX = 0f;
        float rotationSpdY = 0f;
        float rotationSpdZ = 0f;

        // calculate rotation speed based on how far the head is rotated
        if (differenceX < deadZone + 5)
            rotationSpdX = maxTurnSpeed/3;
        else if (differenceX > deadZone + 5 && differenceX < deadZone + 20)
            rotationSpdX = maxTurnSpeed/1.5f;
        else if (differenceX > deadZone + 20)
            rotationSpdX = maxTurnSpeed;

        if (differenceY < deadZone + 5)
            rotationSpdY = maxTurnSpeed/3;
        else if (differenceY > deadZone + 5 && differenceY < deadZone + 20)
            rotationSpdY = maxTurnSpeed/1.5f;
        else if (differenceY > deadZone + 20)
            rotationSpdY = maxTurnSpeed;

        if (differenceZ < deadZone + 5)
            rotationSpdZ = maxTurnSpeed / 3;
        else if (differenceZ > deadZone + 5 && differenceZ < deadZone + 20)
            rotationSpdZ = maxTurnSpeed / 1.5f;
        else if (differenceZ > deadZone + 20)
            rotationSpdZ = maxTurnSpeed;
        //power = chopper.Power;
        // if rotate enough
        if (differenceX > deadZone)
        {
            if (differenceX < 180 && (eyesX - quadX > 0))
            {
                Debug.Log("x > deadzone");
                //chopper.Drive(power, -rotationSpdX, 0, 0);
                transform.Rotate(rotationSpdX * Time.deltaTime, 0, 0);
            }
            else if (differenceX < 180 && (eyesX - quadX < 0))
            {
                //chopper.Drive(power, rotationSpdX, 0, 0);
                transform.Rotate(-rotationSpdX * Time.deltaTime, 0, 0);
            }
            else if (differenceX > 180 && (eyesX - quadX < 0))
            {
                //chopper.Drive(power, -rotationSpdX, 0, 0);
                transform.Rotate(rotationSpdX * Time.deltaTime, 0, 0);
            }
            else if (differenceX > 180 && (eyesX - quadX > 0))
            {
                //chopper.Drive(power, rotationSpdX, 0, 0);
                transform.Rotate(-rotationSpdX * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            //chopper.Drive(power, 0, 0, 0);
            Debug.Log("x not in deadzone");
        }

        

        if (differenceY > deadZone)
        {
            Debug.Log("y > deadzone");
            if (differenceY < 180 && (eyesY - quadY > 0))
            {
                //chopper.Drive(power, 0, -rotationSpdY, 0);
                transform.Rotate(0, rotationSpdY * Time.deltaTime, 0);
            }
            else if (differenceY < 180 && (eyesY - quadY < 0))
            {
                //chopper.Drive(power, 0, rotationSpdY, 0);
                transform.Rotate(0, -rotationSpdY * Time.deltaTime, 0);
            }
            else if (differenceY > 180 && (eyesY - quadY < 0))
            {
                //chopper.Drive(power, 0, -rotationSpdY, 0);
                transform.Rotate(0, rotationSpdY * Time.deltaTime, 0);
            }
            else if (differenceY > 180 && (eyesY - quadY > 0))
            {
                //chopper.Drive(power, 0, rotationSpdY, 0);
                transform.Rotate(0, -rotationSpdY * Time.deltaTime, 0);
            }

        }
        else
        {
            //chopper.Drive(power, 0, 0, 0);
            Debug.Log("y not in deadzone");
        }
        

        if (differenceZ > deadZone)
        {
            Debug.Log("z > deadzone");
            if (differenceZ < 180 && (eyesZ - quadZ > 0))
            {
                //chopper.Drive(power, 0, 0, -rotationSpdZ);
                transform.Rotate(0, 0, rotationSpdZ * Time.deltaTime);
            }
            else if (differenceZ < 180 && (eyesZ - quadZ < 0))
            {
                //chopper.Drive(power, 0, 0, rotationSpdZ);
                transform.Rotate(0, 0, -rotationSpdZ * Time.deltaTime);
            }
            else if (differenceZ > 180 && (eyesZ - quadZ < 0))
            {
                //chopper.Drive(power, 0, 0, -rotationSpdZ);
                transform.Rotate(0, 0, rotationSpdZ * Time.deltaTime);
            }
            else if (differenceZ > 180 && (eyesZ - quadZ > 0))
            {
                //chopper.Drive(power, 0, 0, rotationSpdZ);
                transform.Rotate(0, 0, -rotationSpdZ * Time.deltaTime);
            }

        }
        else
        {
            //chopper.Drive(power, 0, 0, 0);
            Debug.Log("z not in deadzone");
        }

        //Rotate around Y axis
        //transform.Rotate(Vector3.up * Time.deltaTime * speed);

    }
}
