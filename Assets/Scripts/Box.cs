using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Player")) {
            GameObject parentBox = transform.parent.gameObject;
            parentBox.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
