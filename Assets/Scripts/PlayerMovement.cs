using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    #region Movement_variables
    //SerializeField allows private variables to be seen and changed in the inspector
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float maxVelocity = 3f;
    #endregion

    #region Fuel_variables
    [SerializeField] private float initialFuel = 1000f;
    [SerializeField] private float burnRate = 0.01f;
    public float currFuel;
    public Slider FuelSlider;
    #endregion

    #region SFX
    [SerializeField] private AudioSource audioSourceBoost;
    [SerializeField] private AudioSource audioSourceFuel;
    [SerializeField] private AudioSource audioSourceCollide;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currFuel = initialFuel;
        FuelSlider.value = currFuel / initialFuel;

    }
    
    void Update()
    {
        //Get user inputs
        float xAxis = Input.GetAxis("Horizontal");
        Rotate(xAxis * -rotationSpeed);

        if (Input.GetKey(KeyCode.Space) && currFuel > 0) {
            // Better way to find boost effect child?
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            ThrustForward();
            if (!audioSourceBoost.isPlaying) {
                audioSourceBoost.Play();
            }
        } else {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            audioSourceBoost.Stop();
        }

        if (currFuel < initialFuel / 10 && !audioSourceFuel.isPlaying) {
            audioSourceFuel.Play();
        }

        ClampVelocity();
    }

    //Clamp velocity according to max velocity
    private void ClampVelocity() {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    //Apply forward force
    private void ThrustForward() {
        Vector2 force = transform.up * speed;
        rb.AddForce(force);

        // Should we encorperate Time into the burning of fuel, somehow?
        currFuel -= burnRate;
        FuelSlider.value = currFuel / initialFuel;
        // Debug.Log(currFuel);

        if (currFuel <= 0) {
            Lose();
        }
    }

    //Rotate the object
    private void Rotate(float amount) {
        transform.Rotate(0, 0, amount);
    }

    public void Lose()
    {
        // FindObjectOfType<AudioManager>().Play("PlayerDeath");
        // Destroy(this.gameObject);

        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Planet")) {
            audioSourceCollide.Play();
        }
    }
}
