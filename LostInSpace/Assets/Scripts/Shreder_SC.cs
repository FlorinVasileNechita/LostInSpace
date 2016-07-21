using UnityEngine;
using System.Collections;

public class Shreder_SC : MonoBehaviour {

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(this.transform.position, new Vector3(50,10,0));
        Gizmos.DrawWireCube(this.transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Destorying=" + collider.transform.name);
        Debug.Log("aaaaaaaaa=");
        Destroy(collider.gameObject);
    }
}