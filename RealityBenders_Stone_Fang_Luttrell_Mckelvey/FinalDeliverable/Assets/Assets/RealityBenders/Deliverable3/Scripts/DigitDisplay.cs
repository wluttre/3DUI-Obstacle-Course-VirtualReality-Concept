using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DigitDisplay : MonoBehaviour {
    public int digit;
    public Sprite[] digitList;
    SpriteRenderer mySpriteRenderer;
	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        mySpriteRenderer.sprite = digitList[digit];
	}
}
