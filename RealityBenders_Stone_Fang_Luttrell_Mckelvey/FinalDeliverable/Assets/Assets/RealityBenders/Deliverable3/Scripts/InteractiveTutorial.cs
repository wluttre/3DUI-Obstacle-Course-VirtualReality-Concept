using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour {

    public bool requestLeftArm, requestRightArm, requestLeftFoot, requestRightFoot;
    private GameObject leftHand, rightHand, leftFoot, rightFoot;

    private GameObject leftHandVisual, rightHandVisual, leftFootVisual, rightFootVisual;


    // Use this for initialization
    void Start () {
        requestLeftArm = true;
        requestRightArm = false;
        requestLeftFoot = false;
        requestRightFoot = false;

        leftHand = GameObject.Find("Left Hand Logic");
        rightHand = GameObject.Find("Right Hand Logic");
        leftFoot = GameObject.Find("Left Foot Logic");
        rightFoot = GameObject.Find("Right Foot Logic");

        leftHandVisual = GameObject.Find("leftHandTut");
        rightHandVisual = GameObject.Find("rightHandTut");
        leftFootVisual = GameObject.Find("rightShoeTut");
        rightFootVisual = GameObject.Find("leftShoeTut");
    }
	
	// Update is called once per frame
	void Update () {
        if (requestLeftArm == true && leftHand.transform.position.y >= 1.5)
        {
            requestRightArm = true;
            requestLeftArm = false;

            leftHandVisual.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            rightHandVisual.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
        else if(requestRightArm == true && rightHand.transform.position.y >= 1.5)
        {
            requestLeftFoot = true;
            requestRightArm = false;

            rightHandVisual.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            rightFootVisual.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else if (requestLeftFoot == true && leftFoot.transform.position.y >= 0.3)
        {
            requestRightFoot = true;
            requestLeftFoot = false;

            rightFootVisual.GetComponentInChildren<MeshRenderer>().enabled = false;
            leftFootVisual.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else if (requestRightFoot == true && rightFoot.transform.position.y >= 0.3)
        {
            leftFootVisual.GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject.Destroy(this.gameObject);
        }

    }


}
