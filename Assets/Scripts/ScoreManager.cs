using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
	}
}
