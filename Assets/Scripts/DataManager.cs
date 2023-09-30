using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DataManager instance;

    public static float remainingFuel;

    private void Awake()    
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
        DontDestroyOnLoad(gameObject);
    }

    public void setRemainingFuel(float fuel) {
        Debug.Log("setting remaining fuel" + fuel);
        remainingFuel = fuel;
    }


}
