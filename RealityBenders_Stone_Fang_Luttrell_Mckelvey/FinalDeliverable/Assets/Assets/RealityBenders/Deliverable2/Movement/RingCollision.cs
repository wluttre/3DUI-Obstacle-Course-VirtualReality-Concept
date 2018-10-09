using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollision : MonoBehaviour {
    public AudioSource collide;
    bool check = false;
    // Use this for initialization
    void Start () {
        collide = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision ring)
    {
        Debug.Log(ring.gameObject.name+ " has entered the trigger");
        if (ring.gameObject.name == "Hex")
        {
             collide.Play();
            check = true;
            Debug.Log("check response in OnCollisionEnter "+ check );
        }
    }
    // Update is called once per frame
    void Update () {
        if (check == true)
        {
            collide.Play();
            check = false;
            Debug.Log("check response in Update " + check);

        }
    }
}
