using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public LadderTask ladders;
    private float completionPosition = 0.3f;
    public float delay = 10.0f;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
                if (GameObject.Find("Camera (eye)").transform.position.y >= (completionPosition * 16.0f) - 0.001f)

{
            if (delay <= 0.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +
               1);
            }
        }
        if (Input.GetKey("j"))
        {
            //   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Test2");
        }
    }
}