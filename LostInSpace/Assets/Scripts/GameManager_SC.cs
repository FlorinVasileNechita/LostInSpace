using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager_SC : MonoBehaviour {

    public void loadLevel(string levelName) {
        Debug.Log("Loading level named " + levelName);
        try {
            SceneManager.LoadScene(levelName);
        } catch (Exception ex) {
            Debug.LogError("Scene " + levelName + " cannot be found");
        }
    }

    public int getCurrentLevel() {
        int currentSceneNo = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene=" + currentSceneNo);
        return currentSceneNo;
    }

    void Start() {
        Debug.Log("GameManager here, level=" + getCurrentLevel());
    }

    public int getDifficultyIndex() {
        string entireLevelName = SceneManager.GetActiveScene().name;
        Debug.Log("Scene name= " + entireLevelName);
        String[] splitted = entireLevelName.Split('_');
        Debug.Log("DifficultyIndex=" + splitted[2]);
        return Convert.ToInt32(splitted[2]);
    }
}
