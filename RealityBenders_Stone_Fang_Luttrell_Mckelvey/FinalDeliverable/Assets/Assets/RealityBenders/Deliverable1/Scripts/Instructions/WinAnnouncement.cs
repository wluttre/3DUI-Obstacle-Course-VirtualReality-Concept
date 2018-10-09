using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnnouncement : MonoBehaviour
{
    //public AudioSource environment;
    public LadderTask ladders;
    public Texture2D thumbsUp;
    public AudioSource cheer;
    private float completionPosition = 0.3f;
    private bool cheerCheck = false;
    
    // Use this for initialization
    void Start()
    {
        cheer = GetComponent<AudioSource>();
        

        GetComponent<Renderer>().enabled = false;
           
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Camera (eye)").transform.position.y >= (completionPosition * 16) - 0.001f && cheerCheck == false)
        {
            cheerCheck = true;
            GetComponent<Renderer>().enabled = true;
            cheer.Play();
          
        }
        
    }
    
}
