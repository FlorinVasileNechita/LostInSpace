using UnityEngine;
using System.Collections;

public class Player_SC : MonoBehaviour {

    public float speed;

    private float xMin, xMax, offsetSpace = 1f;

    // Use this for initialization
    void Start() {
        Debug.Log("Player_SC started!");
        computePlayersMaxPosition();
    }

    // Update is called once per frame
    void Update() {
        playerMovementController();
        playerMovementByDefault(1f);
    }

    private void computePlayersMaxPosition() {
        Debug.Log("Computing max X and Y for the player");
        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 maxLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        xMin = maxLeft.x + offsetSpace;

        Vector3 maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMax = maxRight.x - offsetSpace;
    }

    private void playerMovementController() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            changePlayerPosition(Vector3.left, speed);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            changePlayerPosition(Vector3.right, speed);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            changePlayerPosition(Vector3.up, speed);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            // changePlayerPosition(Vector3.down, speed);
        }
    }

    private void changePlayerPosition(Vector3 vector3, float speed) {
        this.transform.position += vector3 * speed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
    }

    private void playerMovementByDefault(float levelRunningSpeed) {
        changePlayerPosition(Vector3.up, levelRunningSpeed);
    }





}
