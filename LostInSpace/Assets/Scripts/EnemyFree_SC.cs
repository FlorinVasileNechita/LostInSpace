using UnityEngine;
using System.Collections;

public class EnemyFree_SC : MonoBehaviour {

    public GameObject[] freeEnemies;
    private GameObject enemiesGroup;

    // Use this for initialization
    void Start() {
        enemiesGroup = GameObject.Find("EnemiesGroup");

        //spawnEnemies();
    }

    // Update is called once per frame
    void Update() {
        spawnEnemies();
    }

    private void spawnEnemies() {
        float probability = 0.05f * Time.deltaTime;
        if (Random.value < probability) {
            GameObject enemie = Instantiate(freeEnemies[Random.Range(0, freeEnemies.GetLength(0))], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            enemie.transform.parent = enemiesGroup.transform;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }


}
