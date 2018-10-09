using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    float Speed = 90.0f;
    float Speed2 = 45.0f;
    float acceleration = 20.5f;
    float deceleration = -20.5f;
    public Quadcopter chopper;
    private GameObject LController, RController, Eyes;

    // Use this for initialization
    void Start()
    {
        if (LController == null || RController == null)
        {
            LController = GameObject.Find("Controller (left)");
            RController = GameObject.Find("Controller (right)");
            Eyes = GameObject.Find("Camera (eye)");
           
        }

        //eyes = GameObject.Find("Camera (eye)");
        // y = chopper.transform.position.y;
        //eyes = GameObject.Find("Camera (eye)");

    }


    void Awake()
    {
        if (LController == null || RController == null)
        {
            LController = GameObject.Find("Controller (left)");
            RController = GameObject.Find("Controller (right)");
            Eyes = GameObject.Find("Camera (eye)");

        }
        //eyes = GameObject.Find("Camera (eye)");
        // y = chopper.transform.position.y;
        //eyes = GameObject.Find("Camera (eye)");

    }
    // Update is called once per frame
    void Update()
    {
      // chopper.transform.Rotate(Vector3.right, Speed * Time.deltaTime);

        if (LController == null || RController == null)
        {
            LController = GameObject.Find("Controller (left)");
            RController = GameObject.Find("Controller (right)");
            Eyes = GameObject.Find("Camera (eye)");

        }

/*        Debug.Log(LController.transform.position.x - Eyes.transform.position.x);
        // chopper.transform.Rotate(Time.deltaTime * Speed, 0, 0);
       // chopper.transform.Rotate(Vector3.right, Speed * Time.deltaTime);

        if ((LController.transform.position.x - Eyes.transform.position.x >= 0.4 ))
        {
           // chopper.FlipRoll(-1);
            //if ((RController.transform.position.x - Eyes.transform.position.x >= 0.5))
            // {
            //Speed = Speed + (Time.deltaTime * acceleration);

            // if ((RController.transform.position.x - Eyes.transform.position.x < 0))
            //{
            Speed += 0.5f;
            Debug.Log("we are in here");
            chopper.transform.RotateAround(Vector3.zero,Vector3.up, Speed * Time.deltaTime);
            if (Speed >= 180)
            {
                Speed = 0;

            }
            // }

            // chopper.transform.Rotate(Time.deltaTime * Speed, 0, 0);

            //if (transform.localRotation.x + 0.1f > 2)
            //  transform.localRotation = new Quaternion(transform.localRotation.x + .1f - 1f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
            //else
            //transform.localRotation = new Quaternion(transform.localRotation.x + .1f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
            // }
        }

       // Debug.Log(LController.transform.position.x - Eyes.transform.position.x);

        else if ((LController.transform.localPosition.x - Eyes.transform.localPosition.x <= -0.40))
        {
            //chopper.FlipRoll(1);
            
                Speed += 0.5f;
                chopper.transform.RotateAround(Vector3.zero, Vector3.down, Speed * Time.deltaTime);
                if (Speed >= 180)
                {
                   Speed = 0;
                }
            

        }
       // Debug.Log(LController.transform.position.y - Eyes.transform.position.y);
       //up
        else if ((LController.transform.localPosition.y - Eyes.transform.localPosition.y >= 0.1))
        {
            Speed += 0.5f;

            chopper.transform.Rotate(Vector3.left, Speed * Time.deltaTime);
            if (Speed >= 180)
            {
                Speed = 0;
            }
        }
        //transform.Rotate(Time.deltaTime * Speed, 0, 0);

        //if (transform.localRotation.x + 0.1f > 2)
        //  transform.localRotation = new Quaternion(transform.localRotation.x + .1f - 1f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        //else
        //transform.localRotation = new Quaternion(transform.localRotation.x + .1f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);

    
   // Debug.Log(LController.transform.position.y - Eyes.transform.position.y);
       //down
       else  if ((LController.transform.localPosition.y - Eyes.transform.localPosition.y <= -.49))
        {
            Speed += 0.5f;

            chopper.transform.Rotate(Vector3.right, Speed* Time.deltaTime);
            if (Speed >= 180)
            {
                Speed = 0;
            }
        }
        else 
        {
            Speed = 0;
            Speed2 = 0;
        }*/
       
        /*Keyboard Controls used for Debug Mode, uncomment for use and comment VR controls for use*/
         if ((Input.GetKey("u")))
         {

            Speed += 20f;

            chopper.transform.Rotate(Vector3.right, Speed * Time.deltaTime);
                 if (Speed >= 180)
                 {
                     Speed = 0;
                 }



         }


         if ((Input.GetKey("m")))
         {
            Speed += 20f;


            chopper.transform.Rotate(Vector3.left, Speed * Time.deltaTime);
                 if (Speed >= 180)
                 {
                     Speed = 0;
                 }


         }

         if ((Input.GetKey("h")))
         {

            Speed += 20f;

            chopper.transform.Rotate(Vector3.up, Speed * Time.deltaTime);
             if (Speed >= 180)
             {
                 Speed = 0;
             }
         }


         if ((Input.GetKey("j")))
         {
            Speed += 20f;


            chopper.transform.Rotate(Vector3.down, Speed * Time.deltaTime);
             if (Speed >= 180)
             {
                 Speed = 0;
             }
         }
         else
         {
             Speed = 0;
             Speed2 = 0;
         }
    }
}


