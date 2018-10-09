using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorCollision : MonoBehaviour {
    public Quadcopter chopper;
    public Rigidbody rb;
    //float hover = 10;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //AccelDecel player = chopper.GetComponent<AccelDecel>();
        /*
       if (chopper.transform.position.y <= -65)
        {
            player.Speed = 0;
            // coll.attachedRigidbody.useGravity = false;
            chopper.GetComponent<Rigidbody>().useGravity = false;
             chopper.transform.position = new Vector3(0, -40, 0);
            chopper.transform.Translate(Vector3.up * Time.deltaTime * hover);
            //SceneManager.LoadScene("Test2");
        }*/
        if (chopper.transform.position.y <= -45)
        {
            float yVel = rb.velocity.y + Physics.gravity.y;
            rb.AddForce(0, -yVel, 0, ForceMode.Acceleration);
            rb.AddForce(0, Input.GetAxis("Vertical") * 5, 0);
            //player.Speed = 0;
            // coll.attachedRigidbody.useGravity = false;
            //chopper.GetComponent<Rigidbody>().useGravity = false;
            //chopper.transform.position = new Vector3(0, -40, 0);
            //chopper.transform.Translate(Vector3.up * Time.deltaTime * player.Speed * -7);
           // hover = hover * 2;
            //SceneManager.LoadScene("Test2");
        }
      /*  if( chopper.transform.position.y <= -55)
        {
           // player.Speed = 0;
            chopper.GetComponent<Rigidbody>().useGravity = false;

           // chopper.transform.position = new Vector3(transform.position.x, -45, transform.position.z);
            chopper.transform.Translate(Vector3.up * Time.deltaTime * player.Speed *-15);
        }*/
        else
        {
            chopper.GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
