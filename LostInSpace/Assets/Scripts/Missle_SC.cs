using UnityEngine;
using System.Collections;

public class Missle_SC : MonoBehaviour {

    private int missleDamage;

    // Use this for initialization
    void Start() {
        System.Random random = new System.Random();
        missleDamage = random.Next(1, 10);
    }

    // Update is called once per frame
    void Update() {

    }

    public int getDamage() {
        return missleDamage;
    }
}
