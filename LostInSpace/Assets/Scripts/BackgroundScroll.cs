using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

    public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3(0, Time.time * scrollSpeed, -2);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
