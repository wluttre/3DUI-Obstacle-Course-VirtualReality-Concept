using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INERTIAL SENSING
 * Check head position and rotation. Used to determine when to move legs.
 * Mostly head rotation, little bit of position too.
 * Euler angles gives degrees, but still ran as quaternions in the engine.
 * Using Quaternions for calculations.
 */

/*
 * NOTE: REMOVE TIMERS IF NEEDED.
 * CHECK TO SEE IF NEW RANGE IS BEING SET WRONG, AKA THE NEW STARTING AREA FOR CHECKING MOVEMENT
 */

/* NOTE: ADD A RETURN RANGE TO 0 IF MOVEMENT HASN'T BEEN DETECTED IN X AMOUNT OF SECONDS.
 * MAY HELP WITH LEG ISSUES.
/*
 * EULER ANGLES GO FROM 0 - 359.99
 */

public class WeightShiftTracking : MonoBehaviour {

    private Quaternion formerRotation;
	private bool footMovement = false, leftLegCheck = false, rightLegCheck = false, possibleFootMovement = true;
	private float startingZRotation;
    private float footTimer = 0;
    private float startingPosition = 0;
    private float ladderTaskTimer = .5f, leftLegTimer = 0, rightLegTimer = 0;
    private GameObject LadderTask;
        
    public float footDefaultTime;
	public float rotationZCheck;
	public float footReturnZCheck;
    public float startingRange;
    public float quaternionMultiplier;
    public float startingCone;
    public float ladderCone;

    /* Starting Zone Variables */
    private GameObject leftHand, rightHand, leftFoot, rightFoot;
    private Boolean isStarting = true;

    /* Tutorial object */
    private GameObject startingTutorial;

    void Awake()
    {
        /* Optimize by saving the game object of ladder task to a variable.
         * Can be freely used afterwards at any point.
         */
        LadderTask = GetLadderTask();

        leftHand = GameObject.Find("Left Hand Logic");
        rightHand = GameObject.Find("Right Hand Logic");
        leftFoot = GameObject.Find("Left Foot Logic");
        rightFoot = GameObject.Find("Right Foot Logic");

        // Tutorial object
        startingTutorial = GameObject.Find("Interactive Tutorial");

        // Fix first foot issue
        formerRotation = transform.rotation;
    }


    void Update () {

        isStarting = (leftFoot.transform.position.y == 0 || rightFoot.transform.position.y == 0) ;

        /* Check to see if interactive tutorial still exists. */
        if(startingTutorial != null)
            startingTutorial = GameObject.Find("Interactive Tutorial");

        /*
         * Only allow for leg movement when the player is looking towards the ladder
         */
        if (VisionLadder() == true)
        {
            /*
             * User may accidently call the opposite leg check when rotating their head back to the starting position,
             * so we created a cone that extends from the starting position that disallows the checks from happening.
             * From -startingRange to startingRange, the user will not be able to activate the leg checks.
             */
            if (transform.rotation.z >= startingPosition - startingRange && transform.rotation.z <= startingPosition + startingRange)
            {
                // Dont't check for movement if user hasn't returned to original state.
            }
            /*
             * Foot movement has been detected, so we need to determine whether or not to complete the leg movement.
             * User is given a system defined amount of time (footTimer) to trigger the end foot movement.
             * Will only commence if the starting movement has been detected.
             */
            else if (footMovement == true && footTimer > 0)
            {
                EndFootCheck();
            }
            /*
             * To optimize code, we will check for beginning leg movement only when necessary.
             * So if the headset has changed position, the begins the check to see if the conditions
             * have triggered the beginning of foot movement.
             */
            else if (possibleFootMovement == true && transform.hasChanged)
            {
                StartFootCheck();
            }
            else if (possibleFootMovement == false)
            {
                if (transform.rotation.z >= -startingCone && transform.rotation.z <= startingCone)
                    possibleFootMovement = true;
            }

            /*
             * Update the former rotation during each step.
             * Required for comparisons to see if user has moved head further enough for various conditional checks.
             */
            formerRotation = transform.rotation;
        }
    }
    

    /*
     * Checks to see if the rotation of the headset is great enough to warrant leg movement.
     * If the difference is great enough to trigger leg movement (rotationZCheck), begin checking for end leg 
     * movement and create a timer for completion of the end check.
     * 
     * Right leg and left leg are differentiated through current and former quaternion values.
     */
	private void StartFootCheck()
	{
        rightLegCheck = false;
        leftLegCheck = false;

        var currQuaternionZ = transform.rotation.z * quaternionMultiplier;
        var formerQuaternionZ = formerRotation.z * quaternionMultiplier;

        possibleFootMovement = false;


        //Debug.Log("Rotation check value " + rotationZCheck + " : " + currQuaternionZ + "and " + formerQuaternionZ);

        // Don't move the legs if too close to one of the hands during the starting position (important, only starting position. Otherwise the user falls).

        // Check if rotation (z) differs by at least rotationZCheck degrees from the former rotation
        // Move right leg
        if (currQuaternionZ > formerQuaternionZ && (rightHand.transform.position.y - rightFoot.transform.position.y > .9 || isStarting != true))
        {
            var checkValue = Math.Abs(formerQuaternionZ - currQuaternionZ);

            if (rotationZCheck <= checkValue)
            {
                Debug.Log("right leg is considering moving");
                Debug.Log("Rotation check value " + rotationZCheck + " : " + currQuaternionZ + "and " + formerQuaternionZ + " and rotation z check" + rotationZCheck + " and checked value" + checkValue);
                rightLegCheck = true;

                //Check to see if headset has moved past the rotation check and returned to the original value.
                //If true, move feet.
                startingZRotation = formerRotation.z;
                footTimer = footDefaultTime;
                footMovement = true;
            }
        }
        // Move left leg
        else if (rightHand.transform.position.y - rightFoot.transform.position.y > .9 || isStarting != true)
        {
            var checkValue = Math.Abs(currQuaternionZ - formerQuaternionZ);

            if (rotationZCheck <= checkValue)
            {
                Debug.Log("left leg is considering moving");
                Debug.Log("Rotation check value " + rotationZCheck + " : " + currQuaternionZ + "and " + formerQuaternionZ + " and rotation z check" + rotationZCheck + " and checked value" + checkValue);
                leftLegCheck = true;

                //Check to see if headset has moved past the rotation check and returned to the original value.
                //If true, move feet.
                startingZRotation = formerRotation.z;
                footTimer = footDefaultTime;
                footMovement = true;
            }
        }
        
    }

    /*
     * Checks to see if the return rotation of the headset is great enough to guarantee leg movement.
     * If the difference is great enough to trigger leg movement (based on footReturnZCheck), begin moving the virtual leg.
     * 
     * Right leg and left leg are differentiated through current and former quaternion values.
     */
    private void EndFootCheck()
	{
		var checkLeft = startingZRotation - footReturnZCheck;
		var checkRight = startingZRotation + footReturnZCheck;

        //Interactive tutorial directions.
        if (startingTutorial == null)
        {
            // User has successfully moved their leg up the rung.
            if (transform.rotation.z > checkLeft || transform.rotation.z < checkRight)
            {
                // Move right leg.
                if (rightLegCheck == true && rightLegTimer <= 0 && leftLegTimer <= 0)
                {

                    // Move up or down, based on controller position.

                    if (transform.position.y < GameObject.Find("Right Hand").transform.position.y)
                    {
                        Debug.Log("Right Hand is below vision. Leg is moving upwards.");
                        LadderTask.GetComponent<LadderTask>().RightFootUp();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }
                    else if (GameObject.Find("Controller (right)").GetComponent<ControllerCollision>().startPosition == false && rightFoot.transform.position.y >= .3)
                    {
                        Debug.Log("Right Hand is below vision. Leg is moving downwards.");
                        LadderTask.GetComponent<LadderTask>().RightFootDown();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }

                    footMovement = false;
                    rightLegTimer = ladderTaskTimer;
                }
                // Move left leg.
                else if (leftLegCheck == true && leftLegTimer <= 0 && rightLegTimer <= 0)
                {
                    // Move up or down, based on controller position.   

                    if (transform.position.y < GameObject.Find("Left Hand").transform.position.y)
                    {
                        Debug.Log("Left Hand is above vision. Leg is moving upwards.");
                        LadderTask.GetComponent<LadderTask>().LeftFootUp();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }
                    else if (GameObject.Find("Controller (left)").GetComponent<ControllerCollision>().startPosition == false && leftFoot.transform.position.y >= .3)
                    {
                        Debug.Log("Left Hand is below vision. Leg is moving downwards.");
                        LadderTask.GetComponent<LadderTask>().LeftFootDown();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }

                    footMovement = false;
                    leftLegTimer = ladderTaskTimer;
                }
                // Decrement timers if leg has recently moved
                else
                {
                    if (rightLegTimer >= 0)
                        rightLegTimer -= Time.deltaTime;
                    if (leftLegTimer >= 0)
                        leftLegTimer -= Time.deltaTime;
                }
            }
        }
        else
        {
            // User has successfully moved their leg up the rung.
            if (transform.rotation.z > checkLeft || transform.rotation.z < checkRight)
            {
                // Move right leg.
                if (rightLegCheck == true && rightLegTimer <= 0 && leftLegTimer <= 0 && startingTutorial.GetComponent<InteractiveTutorial>().requestRightFoot == true)
                {

                    // Move up or down, based on controller position.
                    if (transform.position.y < GameObject.Find("Right Hand").transform.position.y)
                    {
                        Debug.Log("Right Hand is below vision. Leg is moving upwards.");
                        LadderTask.GetComponent<LadderTask>().RightFootUp();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }


                    footMovement = false;
                    rightLegTimer = ladderTaskTimer;
                }
                // Move left leg.
                else if (leftLegCheck == true && leftLegTimer <= 0 && rightLegTimer <= 0 && startingTutorial.GetComponent<InteractiveTutorial>().requestLeftFoot == true)
                {
                    // Move up or down, based on controller position.   

                    if (transform.position.y < GameObject.Find("Left Hand").transform.position.y)
                    {
                        Debug.Log("Left Hand is above vision. Leg is moving upwards.");
                        LadderTask.GetComponent<LadderTask>().LeftFootUp();
                        // Audio
                        GameObject.Find("Left Foot").GetComponent<Footstep>().PlayFootstep();
                    }

                    footMovement = false;
                    leftLegTimer = ladderTaskTimer;
                }
                // Decrement timers if leg has recently moved
                else
                {
                    if (rightLegTimer >= 0)
                        rightLegTimer -= Time.deltaTime;
                    if (leftLegTimer >= 0)
                        leftLegTimer -= Time.deltaTime;
                }
            }
        }

		// Decrement timer and check to see if user has ran out of time to officially move foot.
		footTimer -= Time.deltaTime;
		if(footTimer <= 0)
		{
			footMovement = false;
            startingPosition = 0;

        }
	}

    /* Find the ladder task object in order to communicate with commands to move/position legs and hands */
    public GameObject GetLadderTask()
    {
        return GameObject.Find("Ladder Task");
    }

    /* Determine when the ladder is within the view of the player.
     * 
     * Allows for the player to look around the environment without setting off the leg move triggers
     */
    public bool VisionLadder()
    {
      
        Vector3 checkVision = GameObject.Find("Camera (eye)").GetComponent<Camera>().WorldToViewportPoint(GameObject.Find("Ladder").transform.position);
        if (checkVision.x > .05 && checkVision.x < .95)
        {
            Debug.Log("Ladder in view");
            return true;
        }
        else
        {
            Debug.Log("Ladder not in view");
            return false;
        }

    }
}