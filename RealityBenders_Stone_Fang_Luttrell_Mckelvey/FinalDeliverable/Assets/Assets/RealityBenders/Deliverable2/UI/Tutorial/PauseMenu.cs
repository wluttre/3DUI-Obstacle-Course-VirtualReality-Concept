using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts

public class PauseMenu : MonoBehaviour
{
    bool isPaused, menuPressed, trigPressed;
    public SteamVR_TrackedController controller;
    public Image slide1, slide2, slide3;
    int second, tSec ;

    void Start()
    {
        menuPressed = false;
        trigPressed = false;
        slide1.enabled = false;
        slide2.enabled = false;
        slide3.enabled = false;
        second = 60;
        tSec = 0;
        controller = GetComponent<SteamVR_TrackedController>();
        isPaused = false; //make sure isPaused is always false when our scene opens
    }

    void Update()
    {
        Debug.Log("menuPress" + menuPressed);
        //If player presses menu and game is not paused. Pause game. If game is paused and player presses menu, unpause.
        if (controller.menuPressed && !isPaused && second <= 0 && !menuPressed) { 
            menuPressed = true;
            Pause();
        }
        if (controller.menuPressed && isPaused && second <= 0 && !menuPressed)
        {
            menuPressed = true;
            UnPause();
            Debug.Log("unpause");
        }
        if (second > 0)
        {
            second--;
        }
        else if (second <= 0)
        {
            second = 60;
            menuPressed = false;
        }
    
        if (isPaused)
        {
            Debug.Log("trigPress" + trigPressed);
            if (controller.triggerPressed && slide1.enabled && !trigPressed && tSec <=0)
            {

                trigPressed = true;
                slide1.enabled = false;
                slide2.enabled = true;
            }
            else if (controller.triggerPressed && slide2.enabled && !trigPressed && tSec <= 0)
            {
                trigPressed = true;
                slide2.enabled = false;
                slide3.enabled = true;
            }
            else if (controller.triggerPressed && slide3.enabled && !trigPressed && tSec <= 0)
            {
                trigPressed = true;
                slide3.enabled = false;
                slide1.enabled = true;
            }
            else if (tSec > 0)
            {
                tSec--;
            }
            else if (tSec <=0)
            {
                tSec = 60;
                trigPressed = false;
            }
        }

    }

    public void Pause()
    {
        isPaused = true;
        slide1.enabled = true;
        //Time.timeScale = 0f; //pause the game
    }

    public void UnPause()
    {
        isPaused = false;
        slide1.enabled = false;
        slide2.enabled = false;
        slide3.enabled = false;
        //Time.timeScale = 1f; //resume game
    }
}