using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Tooltip("Which planet is it? HomePlanet = 0")]
    [SerializeField] private int planetID;

    public bool boxDelivered;

    [SerializeField] private AudioSource audioSourceDelivered;

    void Start() {
        boxDelivered = false;
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Box")) {
            Destroy(obj);
            boxDelivered = true;
            audioSourceDelivered.Play();
        }
    }

    public bool isBoxDelivered() {
        return boxDelivered;
    }

    public void setBoxDelivered(bool b) {
        boxDelivered = b;
    }
}
