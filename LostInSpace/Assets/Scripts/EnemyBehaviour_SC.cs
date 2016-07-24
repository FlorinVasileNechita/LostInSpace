using UnityEngine;
using System.Collections;

public class EnemyBehaviour_SC : MonoBehaviour {

    private ScoreController_SC scoreController;
    public GameObject projectileTexture;
    private GameObject projectileGroup;
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    private float health = 100f;


    // Use this for initialization
    void Start() {
        scoreController = GameObject.Find("Score").GetComponent<ScoreController_SC>();
        projectileGroup = GameObject.Find("EnemiesProjectiles");
    }

    // Update is called once per frame
    void Update() {
        fire();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("EnemyBehaviour_SC_Destorying=" + collider.transform.name);
        Debug.Log("EnemyBehaviour_SC_");

        Shreder_SC shrederScript = collider.gameObject.GetComponent<Shreder_SC>();
        Player_SC playerScript = collider.gameObject.GetComponent<Player_SC>();
        if (shrederScript) {
            // Enemie hits collider
            Debug.Log("Enemie hit by Shreder");
        } else if (playerScript) {
            // Enemie hits PLAYER
            Debug.Log("Enemie hit by PLAYER");
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(gameObject);
            playerScript.addHealth(-getRandomValues(10,30));
            //TODO add health - for the player
        } else {
            //hitted by players projectile
            Debug.Log("Enemie hit by PLAYER projectile");
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            int value = getRandomValues(10, 30);
            health -= value;
            if (health <= 0f) {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Destroy(gameObject);
                scoreController.addScore(value);
            }
            Destroy(collider.gameObject);
        }
    }

    private int getRandomValues(int low, int hight) {
        System.Random random = new System.Random();
        return random.Next(low, hight);
    }

    private void fire() {
        float proability = 0.2f * Time.deltaTime;
        if (Random.value < proability) {
            GameObject projectile = Instantiate(projectileTexture, transform.position, Quaternion.identity) as GameObject;
            projectile.transform.parent = projectileGroup.transform;
            System.Random random = new System.Random();
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -random.Next(1, 4), 0);
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }


}

