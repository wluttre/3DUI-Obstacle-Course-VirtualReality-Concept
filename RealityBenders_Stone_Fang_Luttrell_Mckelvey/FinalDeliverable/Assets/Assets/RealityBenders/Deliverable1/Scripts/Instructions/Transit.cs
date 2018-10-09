using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Transit : MonoBehaviour
{
    public Sprite slide1 = new Sprite();
    public Sprite slide2 = new Sprite();
    public Sprite slide3 = new Sprite();
    public Sprite slide4 = new Sprite();
    public Sprite slide5 = new Sprite();
    public Sprite slide6 = new Sprite();
    private double delay = 4f;
    private double index = 0;

    // Update is called once per frame
    public void Update()
    {

        if (delay <= 0)
        {
            delay = 4;
            index++;
        }
        else
            delay -= Time.deltaTime;

        if (index>=5){
            index = 0;
        }

        if (index == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide1;
        }

        if (index == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide2;

        }

        if (index == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide3;
        }

        if (index == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide4;
        }

        if (index == 4)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide5;
        }

        if (index == 5)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = slide6;
            delay = 7f;
        }

    }
}