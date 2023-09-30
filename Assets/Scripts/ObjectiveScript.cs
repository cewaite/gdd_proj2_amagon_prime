using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveScript : MonoBehaviour
{
    #region gameplay_variables
    public GameObject player;
    public GameObject[] planets = new GameObject[2];
    public GameObject earth;
    #endregion

    #region UI_variables
    public TextMeshProUGUI objectiveText;
    private string currentObjective;
    public TextMeshProUGUI planetText;
    #endregion


    #region private_variables
    private GameObject currentPlanet;
    private Planet planetScript; 
    private int planetsLeft;

    #endregion

    private static float remainingFuel;

    void Start()
    {
        // Set a starting planet, and current objective.
        currentPlanet = planets[1];
        currentObjective = "Deliver box to " + currentPlanet.name;
        planetsLeft = 1;
        planetText.text = "Planets Left: " + planetsLeft;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        planetScript = currentPlanet.GetComponent<Planet>();

        // Check to see if they can win. Else, do smt else
        if (planetsLeft <= 0) {
            objectiveText.text = "Return to Earth safely to win!";
            if (player.GetComponent<PlayerCargo>().hasBox()) {
                GameManager.Instance.WinGame();
            }
        }

        // If the box has been delivered, set a new planet and update the objective text
        else if (planetScript.boxDelivered) {
            Debug.Log("successful landing!");
            currentPlanet.GetComponent<Planet>().setBoxDelivered(false);
            currentPlanet = planets[Random.Range(0, 2)];
            planetsLeft--;

            objectiveText.text = "Return to Earth for another Box";

            planetText.text = "Planets Left: " + planetsLeft;
        }
        else if (player.GetComponent<PlayerCargo>().hasBox() && !planetScript.boxDelivered) {
            objectiveText.text = "Deliver box to " + currentPlanet.name;
        }
        // lol u loser get another box
        else {
            objectiveText.text = "Return to Earth for another Box";
        }
    }

}
