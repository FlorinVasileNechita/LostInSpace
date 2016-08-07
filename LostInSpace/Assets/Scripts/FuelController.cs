using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FuelController : MonoBehaviour {
    private Player_SC player_SC;
    private int levelTimeDuration;

    private Text fuelText;

    // Use this for initialization
    void Start() {
        levelTimeDuration = Player_SC.levelTimeDuration;
        fuelText = this.GetComponent<Text>();
        fuelText.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        int timeSinceLevelLoad = Convert.ToInt32(Time.timeSinceLevelLoad);
        int percent = Convert.ToInt32((timeSinceLevelLoad * 100f) / levelTimeDuration);
        updateFuelTextLabel(percent);
    }

    private void updateFuelTextLabel(int currentFuelLevel) {
        if (currentFuelLevel >= 0 && currentFuelLevel <= 100) {
            fuelText.text = currentFuelLevel.ToString() + "%";
        }
        if(currentFuelLevel >= 100) {
            if (Convert.ToInt32(Time.timeSinceLevelLoad) % 2 == 0) {
                fuelText.color = Color.green;
            } else {
                fuelText.color = Color.red;
            }
        }
    }
}
