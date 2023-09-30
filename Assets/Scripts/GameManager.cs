using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    #region Unity_functions
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoseGame()
    {
        GameObject[] data = GameObject.FindGameObjectsWithTag("DataManager");
        GameObject dm = data[0];
        PlayerMovement player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        dm.GetComponent<DataManager>().setRemainingFuel(player.currFuel);
        SceneManager.LoadScene("LoseScene");
    }

    public void WinGame()
    {
        GameObject[] data = GameObject.FindGameObjectsWithTag("DataManager");
        GameObject dm = data[0];
        PlayerMovement player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        dm.GetComponent<DataManager>().setRemainingFuel(player.currFuel);
        SceneManager.LoadScene("WinScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
