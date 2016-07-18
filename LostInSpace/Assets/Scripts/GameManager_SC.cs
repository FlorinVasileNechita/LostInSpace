using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager_SC : MonoBehaviour {

    public void loadLevel(string level) {
        Debug.Log("Loading level named " + level);
        SceneManager.LoadScene(level);
    }

    public void loadLevel(int level) {
        Debug.Log("Loading level no " + level);
        SceneManager.LoadScene(level);
    }

    public int getCurrentLevel() {
        int currentSceneNo = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene=" + currentSceneNo);
        return currentSceneNo;
    }
}
