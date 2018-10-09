using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpecificControls : MonoBehaviour
{
    private string sceneName;
    public LadderTask ladders;
    public bool allowed = true;

    // Use this for initialization
    void Start()
    {
     Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame 
    void Update()
    {
        if (sceneName == "LadderTask")
        {
            //GetComponent(GameObject).enabled = true;
        }
    }
}