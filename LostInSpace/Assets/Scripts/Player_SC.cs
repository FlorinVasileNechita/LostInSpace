using UnityEngine;

public class Player_SC : MonoBehaviour {

    private float maxAcceleration = 2f;
    private float fireSpeed = 5f;
    private float xMin, xMax, offsetSpace = 1f;
    private GameObject playerProjectiles_GO;
    public GameObject projectile_GO;

    // Use this for initialization
    void Start() {
        Debug.Log("Player_SC started!");
        computePlayersMaxPosition();
        playerProjectiles_GO = GameObject.Find("PlayerProjectiles");
    }

    // Update is called once per frame
    void Update() {
        playerMovementController();
        playerMovementByDefault(1f);
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

    private void playerMovementController() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            changePlayerPosition(Vector3.left, maxAcceleration);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
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

    private void fire() {
        GameObject fire = Instantiate(projectile_GO, this.transform.position, Quaternion.identity) as GameObject;
        fire.transform.parent = playerProjectiles_GO.transform;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
        // add audio
    }




}
