using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScoreScript : MonoBehaviour {
    public int remainingRings;
    public AudioSource ding;
    //public GUIText scoreText;
	// Use this for initialization
	void Start () {
        remainingRings = 25;
        UpdateScore();
        ding = GetComponent<AudioSource>();
	}

    void OnEnable() {
        RaceRing.OnScore += AddScore;
    }

    void OnDisable() {
        RaceRing.OnScore -= AddScore;
    }
    void AddScore(RaceRing ring) {
        if (remainingRings > 0) {
            remainingRings--;
            UpdateScore();
        }

        //see how this is done in deliverable 1
        ding.Play();
    }
    void UpdateScore() {
        //scoreText.text = "Remaining Rings: " + remainingRings;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
