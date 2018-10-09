using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RungDetection : MonoBehaviour {

	// Check to see if Controller has entered rung area.
	public void OnTriggerEnter(Collider collisionObj)
	{
		checkControllerInteraction(collisionObj);
	}
	// Check to see if Controller has entered rung area.
	public void OnTriggerStay(Collider collisionObj)
	{
		checkControllerInteraction(collisionObj);
	}


	private void checkControllerInteraction(Collider collisionObj)
	{
		// If the object is a Controller, it should contain the component ViveController.
		if(collisionObj.gameObject.GetComponent<WandController>() != null)
		{
			if(collisionObj.gameObject.GetComponent<WandController>().getContTrigDown() == true)
			{
				// Place a (potentially hinge) joint from the rung onto the controller in order to keep arm from moving.
				rungCreateJoint(collisionObj);
			}
			else if(collisionObj.gameObject.GetComponent<WandController>().getContTrigUp() == true)
			{
				if(GetComponent<FixedJoint>() != null)
				{
					// Remove connection to the joint + the joint itself.
					GetComponent<FixedJoint>().connectedBody = null;
					Destroy(GetComponent<FixedJoint>());
				}
			}
		}
	}

	// Attach the Controller to the Rung of the ladder.
	private void rungCreateJoint(Collider collisionObj)
	{
		var colJoint = AddHingeJoint();
		//Connected body not needed for hinge joints.
		colJoint.connectedBody = collisionObj.GetComponent<Rigidbody>();
	}

	// Creates a joint to fix object onto Controller.
	private HingeJoint AddHingeJoint()
	{
		HingeJoint newJoint = gameObject.AddComponent<HingeJoint>();
		
		//The hand will be able to rotate along the rod.
		newJoint.axis = new Vector3(1,0,0);
		// Lower values make it easier for the object to "break" out of the grip of the Controller.
		newJoint.breakForce = 200000;
		newJoint.breakTorque = 200000;
		return newJoint;
	}
}
