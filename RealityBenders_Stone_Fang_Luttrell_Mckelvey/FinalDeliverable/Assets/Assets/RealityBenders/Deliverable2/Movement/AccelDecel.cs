using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelDecel : MonoBehaviour {
public float Speed = 0;
float acceleration = 20.5f;
float deceleration = -5.5f;

   
   // float eyesY = 0;
//private GameObject eyes;

    public Quadcopter chopper;
    public Camera mainCamera;
    private GameObject LController,RController, Eyes;

    // Created by: Alexzander Stone
    private AudioSource windAcceleration;
    //public SteamVR_TrackedObject RController;

    // Use this for initialization
    void Start () {
        LController = GameObject.Find("Controller (left)");
        RController = GameObject.Find("Controller (right)");
        Eyes = GameObject.Find("Camera (eye)");

        // Created by: Alexzander Stone
        windAcceleration = GetComponent<AudioSource>();

        //eyes = GameObject.Find("Camera (eye)");
        // y = chopper.transform.position.y;
        //eyes = GameObject.Find("Camera (eye)");

    }


    void Awake()
    {
        LController = GameObject.Find("Controller (left)");
        RController = GameObject.Find("Controller (right)");
        Eyes = GameObject.Find("Camera (eye)");
        //eyes = GameObject.Find("Camera (eye)");
        // y = chopper.transform.position.y;
        //eyes = GameObject.Find("Camera (eye)");

    }

    // Update is called once per frame
    void Update()
    {
        //chopper.transform.Translate(Vector3.forward * Speed);
        // eyes.transform.position = transform.position;
        //eyesY = eyes.transform.eulerAngles.y;
        if (LController == null || RController == null)
        {
            LController = GameObject.Find("Controller (left)");
            RController = GameObject.Find("Controller (right)");
            Eyes = GameObject.Find("Camera (eye)");

        }
        // if (eyesY >= 0)
        //{
        // chopper.transform.Translate(0, Speed, 0);
        chopper.transform.position = transform.position + mainCamera.transform.forward * Speed * Time.deltaTime;
        

        //}
        //if (eyesY < 0)
        //{
        // chopper.transform.Translate(0, Speed*-1, Speed);
        //}
        //chopper.Drive(1.0f, 0, 0, 0);
        /*  Debug.Log("Wieners");
          chopper.Drive(0, 0, 0, 0);*/
       // Debug.Log(LController.transform.position.z - Eyes.transform.position.z);

        //  Debug.Log(LController.transform.localPosition.z - Eyes.transform.localPosition.z);
        // LController.transform.localPosition.z - Eyes.transform.localPosition.z >= 0.3
        if (LController.transform.position.z - Eyes.transform.position.z   >= 0.3 )
        {
           if (RController.transform.position.z - Eyes.transform.position.z >= 0.3)
            {
            Speed = Speed + (Time.deltaTime * acceleration);

            // chopper.Drive(Speed, chopper.Pitch, chopper.Yaw, chopper.Roll);

             if (Speed > 60)
             {
                 Speed = 60;
             }
            }

        }

       // LController.transform.localPosition.z - Eyes.transform.localPosition.z <= 0.29
             if (LController.transform.localPosition.z - Eyes.transform.localPosition.z <= 0.29)
            {
                if (RController.transform.localPosition.z - Eyes.transform.localPosition.z <= 0.29)
                {
                    Speed = Speed + (Time.deltaTime * deceleration);
                    //chopper.Drive(Speed, 0, 0, 0);

                    if (Speed < 0)
                    {
                        Speed = 0;

                    }

                }
            }
             
             /*Keyboard Controls used for Debug Mode, uncomment for use and comment VR Controls for use*/
       /*  if ((Input.GetKey("o")))
        {
            
                Speed = Speed + (Time.deltaTime * acceleration);

                // chopper.Drive(Speed, chopper.Pitch, chopper.Yaw, chopper.Roll);


                if (Speed > 60)
                {
                    Speed = 60;
                }
            

        }


         if (Input.GetKey("l"))
        {
            
                Speed = Speed + (Time.deltaTime * deceleration);
                //chopper.Drive(Speed, 0, 0, 0);

                if (Speed < 0)
                {
                    Speed = 0;

                }

            
        }*/

        // Sound is based off the current speed at which the user is going.
        // Created by: Alexzander Stone
        windAcceleration.pitch = Mathf.Clamp(Speed / 50f, 0, 2.2f);

    }
}

 
