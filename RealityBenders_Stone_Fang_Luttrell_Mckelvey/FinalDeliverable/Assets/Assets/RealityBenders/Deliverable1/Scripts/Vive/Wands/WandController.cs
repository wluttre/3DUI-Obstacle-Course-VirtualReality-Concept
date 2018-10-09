using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Place onto controller objects within scene. Can highlight multiple objects at the same time to drag components onto them.
 * When calling Controller, it will automatically call both controllers AND define each as their own entity (Controller (left) vs Controller (right)).
 */

/*
 * Controllers will need a Rigidbody and Collider if we wish to interact with objects in the game. Will be required for ladder climbing and box stacking.
 * Remember to check the Box Collider as a trigger.
 */

public class WandController : MonoBehaviour {

	//Begin tracking the controller, and provide access to the controller. Now one needs only to call Controller to access the HTC Vive controllers.
	private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
	{
		get
		{
			return SteamVR_Controller.Input((int)trackedObj.index);
		}
	}

	// Keep track of trigger presses for other methods/obj.
	private bool contTrigDown = false;
	private bool contTrigUp = false;

    /* Constructor used to optimize codes by assigning reference to variables of objects that don't change frequently.
     */
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

    // Update is called once per frame
    void Update () {
		//Check for inputs from the Controller here. The Controller var will include both the left and right hand controllers.
		//So each check will check both hands.
		//Controller Grip/Trigger will be useful for the ladder (depending on which is better set up);

        if (Controller.GetHairTriggerDown())
		{
            setContTrigDown(true);
			setContTrigUp(false);
		}
		else if(Controller.GetHairTriggerUp())
		{
			//Example: Can detach hand from ladder when unpressed.
			setContTrigDown(false);
			setContTrigUp(true);
		}

		//Grip Examples
		if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Example: Can attach hand to ladder when pressed.
		}
		else if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Example: Can detach hand from ladder when unpressed. 
		}

	}

	// Setters and getters for inputs by the controller
	private void setContTrigDown(bool state)
	{
		contTrigDown = state;
	}
	public bool getContTrigDown()
	{
		return contTrigDown;
	}
	private void setContTrigUp(bool state)
	{
		contTrigUp = state;
	}
	public bool getContTrigUp()
	{
		return contTrigUp;
	}

}
