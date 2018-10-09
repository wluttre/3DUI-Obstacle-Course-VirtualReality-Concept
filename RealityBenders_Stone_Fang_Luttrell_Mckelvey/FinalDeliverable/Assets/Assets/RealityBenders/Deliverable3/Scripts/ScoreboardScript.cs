using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardScript : MonoBehaviour {
    public DigitDisplay onesDigit;
    public DigitDisplay tensDigit;
    public RingScoreScript scoreCount;

    /* Timer vars */
    public DigitDisplay secondsOnesDigit;
    public DigitDisplay secondsTensDigit;
    public DigitDisplay minutesOnesDigit;
    public DigitDisplay minutesTensDigit;
    private Timer GameTimer;
    private int time;

    // Use this for initialization
    void Start () {
        GameTimer = GameObject.Find("GameTimer").GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        /* Rings */
        onesDigit.digit = scoreCount.remainingRings % 10;
        tensDigit.digit = scoreCount.remainingRings / 10;

        /* Time */
        time = GameTimer.GetCurrentTime();

        minutesTensDigit.digit = time / 600;
        time -= minutesTensDigit.digit * 600;

        minutesOnesDigit.digit = time / 60;
        time -= minutesOnesDigit.digit * 60;

        secondsTensDigit.digit = time / 10;
        time -= secondsTensDigit.digit * 10;

        secondsOnesDigit.digit = time;
    }
}
