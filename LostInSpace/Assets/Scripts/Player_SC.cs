using UnityEngine;

public class Player_SC : MonoBehaviour {

    public Texture2D leftArrowButton;
    public Texture2D rightArrowButton;
    public Texture2D fireButton;

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
        playerMovementController(KeyCode.F15);
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



    private void playerMovementController(KeyCode keyCode) {
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

    private void fire() {
        GameObject fire = Instantiate(projectile_GO, this.transform.position, Quaternion.identity) as GameObject;
        fire.transform.parent = playerProjectiles_GO.transform;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
        // add audio
    }





    void OnGUI() {
        /*
        if(GUI.RepeatButton(new Rect(15, 15, leftArrowButton.width, leftArrowButton.height), leftArrowButton)) {
            Debug.Log("Left Arrow Pressed");
        }*/

        int distanceBetweenButtons = 30;

        if (GUI.RepeatButton(new Rect(0 + distanceBetweenButtons, Screen.height - leftArrowButton.height / 2 - distanceBetweenButtons, leftArrowButton.width, leftArrowButton.height), leftArrowButton)) {
            Debug.Log("Left Arrow Pressed");
            playerMovementController(KeyCode.A);
        }

        if (GUI.RepeatButton(new Rect(0 + leftArrowButton.width + 2 * distanceBetweenButtons, Screen.height - leftArrowButton.height / 2 - distanceBetweenButtons, leftArrowButton.width, leftArrowButton.height), rightArrowButton)) {
            Debug.Log("Right Arrow Pressed");
            playerMovementController(KeyCode.D);
        }

        if (GUI.Button(new Rect(Screen.width - fireButton.width/2 - distanceBetweenButtons , Screen.height - fireButton.height / 2 - distanceBetweenButtons, fireButton.width, fireButton.height), fireButton)) {
            Debug.Log("Fire Pressed");
            fire();
        }





        /* GUI.Button(new Rect(15, 15, 100, 50), "test"); -> create the button */

        /* button created and is clickable
        if (GUI.Button(new Rect(15, 15, 100, 50), "test")) {
            //TODO: actions
        }*/

        /*
        public Texture2D buttonImage = null;
        if(GUI.Button(new Rect(15, 15, 100, 50), buttonImage){
            // the button has an image
        }*/
        /*
      // Texture2D buttonImage = null;
        //if(GUI.Button(new Rect(15, 15, buttonImage.width, buttonImage.height), buttonImage){
            // the button has an image
        }*/


    }
}











