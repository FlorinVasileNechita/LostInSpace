using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreController_SC : MonoBehaviour {

    public static int finalScore = 0;
    private float partialTimeScore = 0;
    private float partialEnemiesScore = 0;
    private Text scoreTextField;

    // Use this for initialization
    void Start() {
        scoreTextField = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        partialTimeScore = Time.timeSinceLevelLoad;
        //Debug.Log("Adding " + partialTimeScore);
        updateScoreUI();
    }

    public void addScore(float points) {
       // Debug.Log("Adding " + points + " to score");
        partialEnemiesScore += points;
        updateScoreUI();
    }

    private void updateScoreUI() {
        //scoreTextField.text = Convert.ToInt32(score).ToString();
        finalScore = Convert.ToInt32(partialEnemiesScore) + Convert.ToInt32(partialTimeScore);
        scoreTextField.text = finalScore.ToString();
    }
}
