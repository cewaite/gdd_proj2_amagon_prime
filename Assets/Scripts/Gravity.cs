using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Tooltip("The strength at which an object is pulled toward planet center")]
    [SerializeField] private float strength = 1.5f;

    private void OnTriggerStay2D(Collider2D coll) {
        GameObject targetObj = coll.gameObject;
        if (targetObj.GetComponent<Rigidbody2D>() != null) {
            // Debug.Log("Object in Grav Field");
            Vector2 planetPos = transform.parent.transform.position;
            Vector2 targetPos = targetObj.transform.position;

            Vector2 direction = (targetPos - planetPos).normalized;
            float distance = (targetPos - planetPos).magnitude;
            targetObj.GetComponent<Rigidbody2D>().AddForce(-direction * strength / distance * 3);
        }
    }
}
