using System;
using UnityEngine;

public class Player_SC : MonoBehaviour {

    public Texture2D leftArrowButtonTexture;
    private Rect leftArrowButtonRect;
    public Texture2D rightArrowButtonTexture;
    private Rect rightArrowButtonRect;
    public Texture2D fireButtonTexture;
    private Rect fireButtonRect;

    private float maxAcceleration = 2f;
    private float fireSpeed = 5f;
    private float xMin, xMax, offsetSpace = 1f;
    private GameObject playerProjectiles_GO;
    public GameObject projectile_GO;

    // Use this for initialization
    void Start() {
        Debug.Log("Player_SC started!");
        computePlayersMaxPosition();
        generateUiButtons();
        playerProjectiles_GO = GameObject.Find("PlayerProjectiles");
    }

    // Update is called once per frame
    void Update() {
        playerMovementController(KeyCode.F15);
        playerMovementByDefault(1f);
        controllUiButtons();
        fireController();
    }

    private void computePlayersMaxPosition() {
        Debug.Log("Computing max X and Y for the player");
        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 maxLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        xMin = maxLeft.x + offsetSpace;

        Vector3 maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMax = maxRight.x - offsetSpace;
    }

    public void playerMovementController(KeyCode keyCode) {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || keyCode == KeyCode.A) {
            changePlayerPosition(Vector3.left, maxAcceleration);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || keyCode == KeyCode.D) {
            changePlayerPosition(Vector3.right, maxAcceleration);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            changePlayerPosition(Vector3.up, maxAcceleration);
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

    private void fireController() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("fire", 0.001f, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("fire");
        }
    }

    public void fire() {
        GameObject fire = Instantiate(projectile_GO, this.transform.position, Quaternion.identity) as GameObject;
        fire.transform.parent = playerProjectiles_GO.transform;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
        // add audio
    }

    private int[] leftArrowButtonProperties;
    private int[] rightArrowButtonProperties;
    private int[] fireButtonProperties;

    private void generateUiButtons() {
        int distanceBetweenButtons = 30;

        leftArrowButtonProperties = new int[] { 0, Screen.height - leftArrowButtonTexture.height / 2 - distanceBetweenButtons, leftArrowButtonTexture.width, leftArrowButtonTexture.height };
        leftArrowButtonRect = new Rect(leftArrowButtonProperties[0], leftArrowButtonProperties[1], leftArrowButtonProperties[2], leftArrowButtonProperties[3]);

        rightArrowButtonProperties = new int[] { leftArrowButtonTexture.width + 2 * distanceBetweenButtons, Screen.height - leftArrowButtonTexture.height / 2 - distanceBetweenButtons, leftArrowButtonTexture.width, leftArrowButtonTexture.height };
        rightArrowButtonRect = new Rect(rightArrowButtonProperties[0], rightArrowButtonProperties[1], rightArrowButtonProperties[2], rightArrowButtonProperties[3]);

        fireButtonProperties = new int[] { Screen.width - fireButtonTexture.width / 2 - distanceBetweenButtons, Screen.height - fireButtonTexture.height / 2 - distanceBetweenButtons, fireButtonTexture.width, fireButtonTexture.height };
        fireButtonRect = new Rect(fireButtonProperties[0], fireButtonProperties[1], fireButtonProperties[2], fireButtonProperties[3]);
    }

    void OnGUI() {
        GUI.Button(leftArrowButtonRect, leftArrowButtonTexture);
        GUI.Button(rightArrowButtonRect, rightArrowButtonTexture);
        GUI.Button(fireButtonRect, fireButtonTexture);
    }

    void controllUiButtons() {
        foreach (Touch touch in Input.touches) {
            // if (leftRectButton.Contains(touch.position)) {
            if ((touch.phase == TouchPhase.Began) || (touch.phase == TouchPhase.Stationary)) {
                if (verifyTouchedButton(touch, leftArrowButtonProperties)) {
                    Debug.Log("LEFT ARROW PRESSED");
                    playerMovementController(KeyCode.A);
                } else {
                    if (verifyTouchedButton(touch, rightArrowButtonProperties)) {
                        Debug.Log("RIGHT ARROW PRESSED");
                        playerMovementController(KeyCode.D);
                    }
                }
            }
            // FIRE ZONE
            if (verifyTouchedButton(touch, fireButtonProperties)) {
                if (touch.phase == TouchPhase.Began) {
                    fire();
                }
            }
        }
    }

    private bool verifyTouchedButton(Touch currentTouch, int[] buttonProperties) {
        int xStartPosition = buttonProperties[0];
        int xEndPosition = buttonProperties[0] + buttonProperties[2];
        int yStartPosition = buttonProperties[1];
        int yEndPosition = buttonProperties[1] + buttonProperties[3];

        Debug.Log("Touch[" + currentTouch.position.x + "," + currentTouch.position.y + "]");
        Debug.Log("X=[" + xStartPosition + "," + xEndPosition + "]");
        Debug.Log("Y=[" + yStartPosition + "," + yEndPosition + "]");

        if (currentTouch.position.x >= buttonProperties[0] && currentTouch.position.x <= buttonProperties[0] + buttonProperties[2]) {
            //if (currentTouch.position.y >= buttonProperties[1] && currentTouch.position.y <= buttonProperties[1] + buttonProperties[3]) {
            return true;
            //}
        }
        return false;
    }
}











