using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreController_SC : MonoBehaviour {

    public static int score = 0;
    private Text scoreTextField;

    // Use this for initialization
    void Start() {
        scoreTextField = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void addScore(int points) {
        // Debug.Log("Adding " + points + " to score");
        score += points;
        updateScoreUI();
    }

    private void updateScoreUI() {
        scoreTextField.text = score.ToString();
    }

    public void resetScore() {
        score = 0;
        updateScoreUI();
    }
}
