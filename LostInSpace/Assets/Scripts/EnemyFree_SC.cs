using UnityEngine;
using System.Collections;
using System;

public class EnemyFree_SC : MonoBehaviour {

    public GameObject[] freeEnemies;
    private GameObject enemiesGroup;

    private bool enemyDeployed = false;
    private int spawnSeconds = 9, waitTime, spawnRate;
    // Use this for initialization
    void Start() {
        enemiesGroup = GameObject.Find("EnemiesGroup");

        System.Random random = new System.Random();
        waitTime = random.Next(4, 10);
        //spawnEnemies();
    }

    // Update is called once per frame
    void Update() {
        spawnEnemies();
    }

    private void spawnEnemies() {
        /* float probability = 0.05f * Time.deltaTime;
         if (Random.value < probability) {
             GameObject enemie = Instantiate(freeEnemies[Random.Range(0, freeEnemies.GetLength(0))], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
             enemie.transform.parent = enemiesGroup.transform;
         }*/
        int timeSinceLevelLoadedAsInt = Convert.ToInt32(Time.timeSinceLevelLoad);
        if (timeSinceLevelLoadedAsInt > waitTime) {
            if (timeSinceLevelLoadedAsInt % spawnSeconds == 0 && !enemyDeployed) {
                enemyDeployed = true;
                GameObject enemie = Instantiate(freeEnemies[UnityEngine.Random.Range(0, freeEnemies.GetLength(0))], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
                enemie.transform.parent = enemiesGroup.transform;
            }
            if (timeSinceLevelLoadedAsInt % spawnSeconds != 0) {
                enemyDeployed = false;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
