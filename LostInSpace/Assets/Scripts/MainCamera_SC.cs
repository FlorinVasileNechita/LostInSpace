using UnityEngine;
using System.Collections;

public class MainCamera_SC : MonoBehaviour {
    private Transform player_GO_Transform;

    // Use this for initialization
    void Start() {
        player_GO_Transform = GameObject.Find("Player_GO").GetComponent<Transform>();
        if (player_GO_Transform) {
            Debug.Log("player_GO_Transform = OK");
        } else {
            Debug.LogError("player_GO_Transform != OK");
        }
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = new Vector3(0, player_GO_Transform.position.y + 3, this.transform.position.z);
    }
}
