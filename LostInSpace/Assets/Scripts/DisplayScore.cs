using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScore : MonoBehaviour {

    private Text scoreTextField;

	// Use this for initialization
	void Start () {
        scoreTextField = GameObject.Find("Score").GetComponent<Text>();
        scoreTextField.text = ScoreController_SC.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
