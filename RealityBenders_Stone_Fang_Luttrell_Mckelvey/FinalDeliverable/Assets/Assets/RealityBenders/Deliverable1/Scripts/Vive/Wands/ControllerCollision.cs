using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollision : MonoBehaviour {
	//Begin tracking the controller, and provide access to the controller. Now one needs only to call Controller to access the HTC Vive controllers.
	private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    // Ladder Task
    private LadderTask LadderTask;
    private GameObject handModel, missingHandModel;
    private AudioSource Grip;
    public bool startPosition;
    private bool heldHand;
    private bool playAudio = false;
    private float delay = 0.5f;

    //Collision Objects (current collision vs currently holding)
    private GameObject collisionObj, objectInHand;

    // Keep track of the current rung, in order to determine which direction to move arms.
    private float currentRungLocation = 1.2f, prevRungLocation;

    // Tutorial var's
    private GameObject startingTutorial;

	/* Constructor used to optimize code by loading information in before gameplay.
     */
	void Awake()
	{
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        heldHand = true;
        prevRungLocation = 1.2f;
        GetLadderTask();
        if (transform.name == "Controller (left)")
        {
            handModel = transform.Find("Final L Hand ver 8").gameObject;
            missingHandModel = transform.Find("Left Missing Hand Indicator").gameObject;
        }
        else if (transform.name == "Controller (right)")
        {
            handModel = transform.Find("Final R Hand ver 8").gameObject;
            missingHandModel = transform.Find("Right Missing Hand Indicator").gameObject;
        }
        startPosition = true;

        // Hide the missing hand indicators until user has attached the real hands to the ladder.
        missingHandModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        // Tutorial object
        startingTutorial = GameObject.Find("Interactive Tutorial");
    }

    void Update()
    {
        if (prevRungLocation % .3f != 0)
        {
            var range = (prevRungLocation / .3f);
            range = (float)Math.Round(range);
            prevRungLocation = range * .3f;
        }

        // When trigger is held down, check to see if valid collisionObj exists. Then grab it.
        if (Controller.GetHairTriggerDown())
        {
            if (collisionObj != null)
            {
                if (collisionObj.transform.tag == "Rung")
                {
                    Debug.Log("COLLISION DETECTION AT: " + collisionObj.name);
                    if (collisionObj.transform.parent.name == "Ladder")
                    {
                        GrabObject();
                    }
                }
                else if (collisionObj.transform.tag == "Limb")
                {
                    Debug.Log("Collision with hand");
                    GrabHand();
                }
            }
        }
        else if (Controller.GetHairTriggerUp())
        {
            if(objectInHand)
                ReleaseObject();
            
        }

        // Audio feedback
        if (playAudio == true)
        {
            delay -= Time.deltaTime;
            if (heldHand == false)
            {
                Debug.Log("delay is " + delay);
                playAudio = false;
                delay = 0.5f;
            }
            else if (delay <= 0)
            {
                GetComponent<AudioSource>().Play();
                delay = 0.5f;
                playAudio = false;
            }

        }

        // Checking for 3 inputs
        if (heldHand == false)
        {
            if(GameObject.Find("Controller (left)").GetComponent<ControllerCollision>().heldHand == false && GameObject.Find("Controller (right)").GetComponent<ControllerCollision>().heldHand == false)
            {
                Debug.Log("3 points of contact lost. Both hands have lost grip.");

                ReleaseObject();



                GetLadderTask().GetComponent<LadderTask>().LeftFootUp();
                GetLadderTask().GetComponent<LadderTask>().RightFootUp();
                GetLadderTask().GetComponent<LadderTask>().LeftFootDown();
                GetLadderTask().GetComponent<LadderTask>().RightFootDown();

                // reset
                GameObject.Find("Controller (left)").GetComponent<ControllerCollision>().heldHand = true;
                GameObject.Find("Controller (right)").GetComponent<ControllerCollision>().heldHand = true;
            }
            
        }

        // Starting position
        if((GameObject.Find("Left Foot").transform.position.y <= 0 || GameObject.Find("Right Foot").transform.position.y <= 0) && startPosition == false)
        {
            startPosition = true;
            heldHand = true;

            HandleFalling();

            /*
            if (name == "Controller (left)")
            {
                prevRungLocation = GameObject.Find("Left Hand").transform.position.y ;
                
            }
            else if (name == "Controller (right)")
            {
                prevRungLocation = GameObject.Find("Right Hand").transform.position.y;
                
            }
            */
            
        }
    }

    /* Falling decisions. */
    private void HandleFalling()
    {
        float fallDistance = 0.0f;
        if (GameObject.Find("Left Foot").transform.position.y < GameObject.Find("Right Foot").transform.position.y)
        {
            fallDistance = GameObject.Find("Left Foot").transform.position.y;
        }
        else
        {
            fallDistance = GameObject.Find("Right Foot").transform.position.y;
        }
        prevRungLocation = transform.position.y - fallDistance;
        currentRungLocation = transform.position.y - fallDistance;

        Debug.Log("fall distance : " + fallDistance);
       
    }

    /* Rather than constantly checking for Inputs before checking collision, this will allow for input detection following collision detection.
     * Used for optimization.
     */
    private void SetCollidingObject(Collider collide)
	{
		//Check to see if object is already holding onto something. If not, then collisionObj should be null (doesn't exist). 
		//Also check to see if collision obj has physics.
		if(collisionObj || !collide.GetComponent<Rigidbody>())
		{
			return;
		}
		//If we passed both checks, assign the obj as the collisionObj (potential for manipulation).
		collisionObj = collide.gameObject;
	}

    /* Optimize code by saving a reference of controller to an object.
     * The reference can then be accessed at any point within the controller object.
     */
    public SteamVR_TrackedController GetController()
    {
        return gameObject.GetComponent<SteamVR_TrackedController>();
    }

    /* 
     * Finds the current collision object when entered, stayed within, and exited.
     * 
     * Useful for determining when to grab the objects.
     */
    public void OnTriggerEnter(Collider collision)
	{
        Debug.Log("COLLISION DETECTION AT: " + collision.name);
		SetCollidingObject(collision);
	}
	public void OnTriggerStay(Collider collision)
	{
        Debug.Log("COLLISION DETECTION AT: " + collision.name);
        SetCollidingObject(collision);
	}
	public void OnTriggerExit(Collider collision)
	{

		// Set the currently held object to null if one exists.
		if(!collisionObj)
		{
			return;
		}
        Debug.Log("COLLISION DETECTION AT: " + collision.name);
        collisionObj = null;
	}

	/* Attach the colliding obj to the controller "hand". Leave the image of the hand behind in order to show player where they are grabbing.
     * When triggered, the system will begin to move the logical hands to the new position. The new position is based on the location of the virtual hands.
     * Also determines when audio is needed to be played.
     * 
     * Checking for "allowed" objects can be done through calls to this method.
     */
	private void GrabObject()
	{
        heldHand = true;  
        playAudio = true;
        

        // Set the new current Rung
        currentRungLocation = collisionObj.transform.position.y;
		objectInHand = collisionObj;
		collisionObj = null;

        // Freeze controller
        handModel.transform.parent = null;

        /* Add the missing hand object to the controller object */
        missingHandModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

        // Joints allow the object to be connected to the Controller. Needed for movement. Var is faster than typing full data type for each variable.
        var colJoint = AddFixedJoint();
		colJoint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        Debug.Log("Next rung : " + currentRungLocation + " and former rung : " + prevRungLocation);

        // Determine which arm should move, and where to.
        if (name == "Controller (left)")
        {
            //Interactive tutorial directions.
            if (startingTutorial == null)
            {
                if (Math.Round(currentRungLocation * 10) > Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().LeftHandUp();
                    prevRungLocation += .3f;
                }
                else if (Math.Round(currentRungLocation * 10) < Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().LeftHandDown();
                    prevRungLocation -= .3f;
                }
                else
                    prevRungLocation = GameObject.Find("Left Hand").transform.position.y;
            }
            else if (startingTutorial.GetComponent<InteractiveTutorial>().requestLeftArm == true)
            {
                if (Math.Round(currentRungLocation * 10) > Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().LeftHandUp();
                    prevRungLocation += .3f;
                }
                else
                    prevRungLocation = GameObject.Find("Left Hand").transform.position.y;
            }
        }
        else if(name == "Controller (right)")
        {
            //Interactive tutorial directions.
            if (startingTutorial == null)
            {
                if (Math.Round(currentRungLocation * 10) > Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().RightHandUp();
                    prevRungLocation += .3f;
                }
                else if (Math.Round(currentRungLocation * 10) < Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().RightHandDown();
                    prevRungLocation -= .3f;
                }
                else
                    prevRungLocation = GameObject.Find("Right Hand").transform.position.y;
            }
            else if (startingTutorial.GetComponent<InteractiveTutorial>().requestRightArm == true)
            {
                if (Math.Round(currentRungLocation * 10) > Math.Round(prevRungLocation * 10))
                {
                    GetLadderTask().GetComponent<LadderTask>().RightHandUp();
                    prevRungLocation += .3f;
                }
                else
                    prevRungLocation = GameObject.Find("Right Hand").transform.position.y;
            }
        }

        if((GameObject.Find("Left Foot").transform.position.y != 0 && GameObject.Find("Right Foot").transform.position.y != 0))
            startPosition = false;
    }

	/*
     * Creates a joint to be used when player grabs a rung.
     * Helps to prevent movement of the hand to show grip.
     */
	private FixedJoint AddFixedJoint()
	{
		FixedJoint newJoint = gameObject.AddComponent<FixedJoint>();
        
		// Lower values make it easier for the object to "break" out of the grip of the Controller.
		newJoint.breakForce = 200000;
		newJoint.breakTorque = 200000;
		return newJoint;
	}


    /*
     * When player releases a rung, proceed to return view of the hands back to the player.
     * Allows for new objects to be placed into the player's hand.
     */
	private void ReleaseObject()
	{
        heldHand = false;

        if(objectInHand)
            Debug.Log("OBJECT IN HAND = " + objectInHand.name);
        // Check to see if object is connected to the controller.
        if (GetComponent<FixedJoint>())
		{
			// Remove connection to the joint + the joint itself.
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
		}

        // Unfreeze hand
        missingHandModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        handModel.transform.parent = transform;
        handModel.transform.localPosition = new Vector3(0, 0, -1);
        handModel.transform.localRotation = new Quaternion(0.7071f, 0, 0, 0.7071f);

        // Remove reference to object in hand.
        objectInHand = null;

	}

    /* 
     * If player attempts to grab the same rung as they had previously grabbed, there is no need to move the arm.
     * Instead, just grab the previous hand location again.
     */
    public void GrabHand()
    {
        heldHand = true;
        playAudio = true;

        objectInHand = collisionObj;

        // Freeze controller
        handModel.transform.parent = null;

        /* Add the missing hand object to the controller object */
        missingHandModel.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }

    /* Find the ladder task object in order to communicate with commands to move/position legs and hands */
    public GameObject GetLadderTask()
    {
        return GameObject.Find("Ladder Task");
    }
}
