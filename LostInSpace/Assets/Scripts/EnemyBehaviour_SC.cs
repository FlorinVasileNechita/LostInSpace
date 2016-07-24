using UnityEngine;
using System.Collections;

public class EnemyBehaviour_SC : MonoBehaviour {

    private ScoreController_SC scoreController;
    public AudioClip hitSound;

    private float health = 100f;


    // Use this for initialization
    void Start() {
        scoreController = GameObject.Find("Score").GetComponent<ScoreController_SC>();
    }   

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("EnemyBehaviour_SC_Destorying=" + collider.transform.name);
        Debug.Log("EnemyBehaviour_SC_");
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Destroy(collider.gameObject);
        health -= 30;
        if (health <= 0f) {
            Destroy(gameObject);
            scoreController.addScore(333);
        }
    }
}

