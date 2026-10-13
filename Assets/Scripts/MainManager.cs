using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class MainManager : MonoBehaviour 
{
    public static MainManager Instance; // instantiates a game object from the MainManager class to be used 

    // Declaring all the variables that need to persist between the scenes
    // These variables must be all public so they can be accessed from each of the menu scripts
    
    public string gamemodechoice; // used to hold the choice of which gamemode was selected

    public string weaponchoice1; // holds the weapon choice of player one

    public string abilitychoice1; // holds the ability choice of player one
    
    public string weaponchoice2; // holds the weapon choice of player two

    public string abilitychoice2; // hold the ability choice of player two

    public int numOfTimesGoneThroughMenu = 0; // holds the number of times the weapon and ability menu has gone through to decided how many more times to go through

    

    private void Awake() // The "Awake" key method is called when the object is created
    {
        if (Instance != null) // checks if this object already exists to prevent the creation of more than one gameObject
        {
            Destroy(gameObject); // destroys this gameobject once it detects that one already exists
            return; // returns nothing to exit the method
        }
        Instance = this; // stores the game object this script is attached to in the "Instance" variable
        DontDestroyOnLoad(gameObject);    // When this scene is unloaded and the next scene is loaded, this game object will be preserved.
    }
}