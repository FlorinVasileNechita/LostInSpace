using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_SC : MonoBehaviour {

    public Texture2D leftArrowButtonTexture;
    private Rect leftArrowButtonRect;
    public Texture2D rightArrowButtonTexture;
    private Rect rightArrowButtonRect;
    public Texture2D fireButtonTexture;
    private Rect fireButtonRect;
    public Texture2D startButtonTexture;
    private Rect startButtonRect;

    public int health;
    private Text healthLabel;

    public static int levelTimeDuration = 60;

    public AudioClip lowHealth;
    private bool lowHealthIsPlaying = false;
    private bool levelIsCompleted = false;

    private float maxAcceleration = 2f;
    private float fireSpeed = 5f;
    private float xMin, xMax, offsetSpace = 1f;
    private GameObject playerProjectiles_GO;
    public GameObject projectile_GO;

    public AudioClip fireSound;

    private GameManager_SC gameManager_SC;

    private float ratioX, ratioY;

    // Use this for initialization
    void Start() {

        ratioX = Screen.width / 480f;
        ratioY = Screen.height / 800f;
        Debug.Log("ratioX =" + ratioX + "   ratioY=" + ratioY);
        Debug.Log("Player_SC started!");
        computePlayersMaxPosition();
        generateUiButtons();
        playerProjectiles_GO = GameObject.Find("PlayerProjectiles");
        healthLabel = GameObject.Find("Life").GetComponent<Text>();
        gameManager_SC = GameObject.Find("GameManager_GO").GetComponent<GameManager_SC>();
        healthLabel.text = health.ToString();

        gameManager_SC.getDifficultyIndex();
    }

    // Update is called once per frame
    void Update() {
        playerMovementController(KeyCode.F15);
        //playerMovementByDefault(1f);
        playerMovementByDefault2(1.5f);
        controllUiButtons();
        fireController();
        playerIsDead();
        updateHealthLabel();
        checkLowHealth();

        levelCompeted();
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
            //changePlayerPosition(Vector3.up, maxAcceleration);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            // changePlayerPosition(Vector3.down, speed);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
            liftOffButtonPressed();
        }
    }

    private void changePlayerPosition(Vector3 vector3, float speed) {
        transform.position += vector3 * speed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void playerMovementByDefault2(float levelRunningSpeed) {
        changePlayerPosition(new Vector3(0,levelRunningSpeed, 0), levelRunningSpeed);
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
        GameObject fire = Instantiate(projectile_GO, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
        fire.transform.parent = playerProjectiles_GO.transform;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private int[] leftArrowButtonProperties;
    private int[] rightArrowButtonProperties;
    private int[] fireButtonProperties;
    private int[] startButtonProperties;

    private void generateUiButtons() {
        int distanceBetweenButtons = 40;
        int scaleDefaultFactor = 90;

        //leftArrowButtonProperties = new int[] { 0, Screen.height - 90 / 2 - distanceBetweenButtons, 90, 90 };
        leftArrowButtonProperties = new int[] { 0, Screen.height - scaleDefaultFactor / 2 - distanceBetweenButtons, Convert.ToInt32(scaleDefaultFactor * ratioX), Convert.ToInt32(scaleDefaultFactor * ratioY) };
        leftArrowButtonRect = new Rect(leftArrowButtonProperties[0], leftArrowButtonProperties[1], leftArrowButtonProperties[2], leftArrowButtonProperties[3]);

        //rightArrowButtonProperties = new int[] { leftArrowButtonTexture.width + 2 * distanceBetweenButtons, Screen.height - 90 / 2 - distanceBetweenButtons, 90, 90 };
        rightArrowButtonProperties = new int[] { leftArrowButtonTexture.width + 2 * distanceBetweenButtons, Screen.height - scaleDefaultFactor / 2 - distanceBetweenButtons, Convert.ToInt32(scaleDefaultFactor * ratioX), Convert.ToInt32(scaleDefaultFactor * ratioY) };
        rightArrowButtonRect = new Rect(rightArrowButtonProperties[0], rightArrowButtonProperties[1], rightArrowButtonProperties[2], rightArrowButtonProperties[3]);

        // fireButtonProperties = new int[] { Screen.width - fireButtonTexture.width / 2 - distanceBetweenButtons, Screen.height - fireButtonTexture.height / 2 - distanceBetweenButtons, fireButtonTexture.width, fireButtonTexture.height };
        fireButtonProperties = new int[] { Screen.width - scaleDefaultFactor / 2 - distanceBetweenButtons-5, Screen.height - 160 / 2 - distanceBetweenButtons, Convert.ToInt32(scaleDefaultFactor * ratioX), Convert.ToInt32(120 * ratioY) };
        fireButtonRect = new Rect(fireButtonProperties[0], fireButtonProperties[1], fireButtonProperties[2], fireButtonProperties[3]);

        // startButtonProperties = new int[] { Screen.width - startButtonTexture.width / 2 - distanceBetweenButtons, Screen.height / 2 , startButtonTexture.width, startButtonTexture.height };
        startButtonProperties = new int[] { (rightArrowButtonProperties[0] + fireButtonProperties[0]) / 2, Screen.height - scaleDefaultFactor / 2 - distanceBetweenButtons-20, Convert.ToInt32(scaleDefaultFactor * ratioX), Convert.ToInt32(120 * ratioY) };
        startButtonRect = new Rect(startButtonProperties[0], startButtonProperties[1], startButtonProperties[2], startButtonProperties[3]);


    }

    void OnGUI() {
        GUI.Button(leftArrowButtonRect, leftArrowButtonTexture);
        GUI.Button(rightArrowButtonRect, rightArrowButtonTexture);
        GUI.Button(fireButtonRect, fireButtonTexture);

        if (levelIsCompleted) {
            GUI.Button(startButtonRect, startButtonTexture);
        }
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
            // START BUTTON ZONE
            if (levelCompeted()) {
                if (verifyTouchedButton(touch, startButtonProperties)) {
                    if (touch.phase == TouchPhase.Began) {
                        Debug.Log("LIFT OFF button PRESSED");
                        liftOffButtonPressed();
                    }
                }
            }
        }
    }

    private void liftOffButtonPressed() {
        gameManager_SC.loadLevel("04_WinScene");
    }

    private bool verifyTouchedButton(Touch currentTouch, int[] buttonProperties) {
        int xStartPosition = buttonProperties[0];
        int xEndPosition = buttonProperties[0] + buttonProperties[2];
        int yStartPosition = buttonProperties[1];
        int yEndPosition = buttonProperties[1] + buttonProperties[3];

        Debug.Log("Touch[" + currentTouch.position.x + "," + currentTouch.position.y + "]" + "   " + "X=[" + xStartPosition + "," + xEndPosition + "]   " + "Y=[" + yStartPosition + "," + yEndPosition + "]");

        if (currentTouch.position.x >= buttonProperties[0] && currentTouch.position.x <= buttonProperties[0] + buttonProperties[2]) {
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Collider detected");
        Missle_SC missle = collider.gameObject.GetComponent<Missle_SC>();
        if (missle) {
            health -= missle.getDamage();
            Destroy(collider.gameObject);
        }
    }

    private void playerIsDead() {
        if (health <= 0) {
            Debug.Log("Player dead!");
            Destroy(gameObject);
            gameManager_SC.loadLevel("05_LoseScene");
        }
    }

    private void updateHealthLabel() {
        if (health < 0) {
            health = 0;
        }
        healthLabel.text = health.ToString();
        if (health <= 40) {
            healthLabel.color = Color.red;
        } else {
            if (health > 40) {
                healthLabel.color = Color.green;
            }
        }
    }

    public void addHealth(int value) {
        health += value;
    }

    private void checkLowHealth() {
        if (health <= 40) {
            if (!lowHealthIsPlaying) {
                lowHealthIsPlaying = true;
                AudioSource.PlayClipAtPoint(lowHealth, transform.position);
            }
        } else if (health > 40) {
            if (lowHealthIsPlaying) {
                lowHealthIsPlaying = false;
            }
        }
    }

    private Boolean levelCompeted() {
        //Debug.Log("Time since level is loaded = " + Time.timeSinceLevelLoad);
        if (Time.timeSinceLevelLoad >= levelTimeDuration) {
            levelIsCompleted = true;
            return true;
        }
        return false;
    }


}











