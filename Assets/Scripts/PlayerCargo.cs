using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCargo : MonoBehaviour
{
    [Tooltip("The initial number of boxes in the Ships cargo")]
    [SerializeField] private int initialNumBoxes = 0;

    [Tooltip("The max number of boxes in the Ships cargo")]
    [SerializeField] private int boxLimit = 1;

    [Tooltip("The box which spawns when pressing J to release cargo")]
    [SerializeField] private GameObject boxPrefab;

    private int currNumBoxes;

    #region SFX
    [SerializeField] private AudioSource audioSourcePickup;
    [SerializeField] private AudioSource audioSourceDrop;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currNumBoxes = initialNumBoxes;
        if (currNumBoxes > 0) {
            transform.Find("CargoIndicator").gameObject.SetActive(true);
        } else {
            transform.Find("CargoIndicator").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.J) && currNumBoxes > 0) {
            currNumBoxes -= 1;
            GameObject box = Instantiate(boxPrefab, transform.position, transform.rotation);
            // GameObject box = Instantiate(boxPrefab, transform.Find("CargoDropMarker").position, transform.rotation);
            box.GetComponent<BoxCollider2D>().enabled = false;
            box.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
            
            if (currNumBoxes == 0) {
                transform.Find("CargoIndicator").gameObject.SetActive(false);
            }

            audioSourceDrop.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Box") && currNumBoxes < boxLimit) {
            Destroy(obj);
            currNumBoxes += 1;
            transform.Find("CargoIndicator").gameObject.SetActive(true);
            audioSourcePickup.Play();
        }
    }

    public bool hasBox() {
        return currNumBoxes > 0;
    }
}
