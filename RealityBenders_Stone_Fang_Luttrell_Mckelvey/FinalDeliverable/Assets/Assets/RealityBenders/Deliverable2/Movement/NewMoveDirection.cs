using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveDirection : MonoBehaviour {

    public float speed;
    private float yDeadZone = .1f; //degrees
    private float xDeadZone = 0.1f; //degrees
    private GameObject eyes, LController, RController;
    public Quadcopter chopper;

    // Use this for initialization
    void Start()
    {
        LController = GameObject.Find("Controller (left)"); // left Controller
        RController = GameObject.Find("Controller (right)"); // right COntroller
        eyes = GameObject.Find("Camera (eye)"); // eyes position/head
        eyes.transform.rotation = transform.rotation;
        //GetComponent<Quadcopter>().gyroStabilization = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LController == null || RController == null)
        {
            LController = GameObject.Find("Controller (left)");
            RController = GameObject.Find("Controller (right)");
            eyes = GameObject.Find("Camera (eye)");
        }
        else
        {
            //Debug.Log(LController.transform.position.x - eyes.transform.position.x);
            //Debug.Log("lcontrol y pos" + LController.transform.position.y);

        }

        transform.rotation= new Quaternion(transform.rotation.x,transform.rotation.y,0,transform.rotation.w);

        //Debug.Log("head y pos" + eyes.transform.position.y);

        // update head to be the same position and rotation has quadcopter
        //eyes.transform.position = transform.position;
        //eyes.transform.rotation = transform.rotation;

        //Debug.Log("test right: value is " + (LController.transform.position.x - eyes.transform.position.x) + " and " + (RController.transform.position.x - eyes.transform.position.x));

        // check to turn left and right
        if ((LController.transform.position.x - eyes.transform.position.x > xDeadZone) && (RController.transform.position.x - eyes.transform.position.x > xDeadZone))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0); //right?
            Debug.Log("turning Right");
        }
        else if (LController.transform.position.x - eyes.transform.position.x < -xDeadZone && RController.transform.position.x - eyes.transform.position.x < -xDeadZone)
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0); //left?
           Debug.Log("turning Left");
        }
        // check to turn down or up   
        if (LController.transform.position.y - eyes.transform.position.y < -yDeadZone && RController.transform.position.y - eyes.transform.position.y < -yDeadZone)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0); //down?
            Debug.Log("turning Up");
        }
        else if (LController.transform.position.y - eyes.transform.position.y > yDeadZone && RController.transform.position.y - eyes.transform.position.y > yDeadZone)
        {
            transform.Rotate(-speed * Time.deltaTime, 0, 0); //up?
            Debug.Log("turning Down");
        }
           
    }
}
