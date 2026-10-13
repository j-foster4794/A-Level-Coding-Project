using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{ 
    // Declaring variables
    public float maxHealth = 200;
    public float health;
    public string weaponChoice;
    public string abilityChoice;
    public string player;
    //Declaring a reference to the MainManager game object
    public GameObject MainManager;

    


    public void Awake()
    {
        // Sets the health equal to the max health
        health = maxHealth; 
        MainManager = GameObject.Find("MainManager"); // Finds the main manager object and stores it
        var MainManagerScript = MainManager.GetComponent<MainManager>(); // Fetches the script component of the main manager and stores it
        // Checks what player it is and sets the ability and weapon choices to the corresponding one
        if (player == "Player") 
        {
            weaponChoice = MainManagerScript.weaponchoice1;
            abilityChoice = MainManagerScript.abilitychoice1;
        }
        else
        {
            weaponChoice = MainManagerScript.weaponchoice2;
            abilityChoice = MainManagerScript.abilitychoice2;
        }
        // Fetches the firing script attached to the player and intialises values necessary for it
        var FiringScript = gameObject.GetComponent<FiringScript>();
        FiringScript.Initialise(weaponChoice, player);
    }
}