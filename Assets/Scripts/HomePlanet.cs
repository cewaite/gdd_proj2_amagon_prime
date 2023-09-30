using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlanet : MonoBehaviour
{
    [Tooltip("The box which spawns when the player lands")]
    [SerializeField] private GameObject boxPrefab;

    private void OnCollisionEnter2D(Collision2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Player")) {
            if (!obj.GetComponent<PlayerCargo>().hasBox()) {
                Instantiate(boxPrefab, obj.transform.position, transform.rotation);
            }
        }
    }
}
