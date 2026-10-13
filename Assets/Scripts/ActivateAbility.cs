using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActivateAbility : MonoBehaviour
{
    // Declaring and assigning variables to be later used
    [SerializeField] string abilityChoice;
    public float cooldown;
    public float lastAbilityUse = 0f;
    [SerializeField] float dashDistance = 10f;
    
    // Declaring variables to store references to components and game objects 
    CharacterController characterController;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject grenadePrefab;

    // Creating delegate types to allow methods to be stored
    public delegate bool CanUseAbilityChecks(); 
    CanUseAbilityChecks CanUseAbility;

    public delegate void UseAbilityMethods();
    UseAbilityMethods UseAbilityMethod;
    
    public bool CanKeyboardPlayerUseAbility() // Declaring function to determine whether the keyboard player is able to use their ability
    {
        if (Input.GetKeyDown(KeyCode.E) && (Time.time - lastAbilityUse) > cooldown) // Checks if it has been long enough and if they are pressing the key
        {
            lastAbilityUse = Time.time; // Sets the last time an ability was used to the current time
            return true; 
        }
        return false;
    }

    public bool CanControllerPlayerUseAbility() // Declaring function to determine whether the controller player is able to use their ability
    {
        if (Input.GetButton("Ability")  && (Time.time - lastAbilityUse) > cooldown) // Checks if it has been long enough and if they are pressing the button
        {
            lastAbilityUse = Time.time; // Sets the last time an ability was used to the current time
            return true;
        }
        return false;
    }

    public void Dash() // Declaring function to make the player dash
    {
        characterController.Move(gameObject.transform.forward * dashDistance); 
    }

    public void Grenade() // Declaring function to create a grenade
    {
        GameObject grenade = Instantiate(grenadePrefab,playerCamera.transform.position, playerCamera.transform.rotation); // Creation of a grenade prefab at the camera position with the rotation of the camera
        var grenadeScript = grenade.GetComponent<Grenade>(); // Returns the script called "Grenade" from the grenade object and stores it
        grenadeScript.Initialise(playerCamera.transform.forward * 30); // Initialises the grenade with a speed
    }

    void Start()
    {
        // Fetches the scripts and components necessary
        characterController = gameObject.GetComponent<CharacterController>();
        var PlayerInfoScript = gameObject.GetComponent<PlayerInfo>();
        abilityChoice = PlayerInfoScript.abilityChoice;

        // Checks which player this script is attached to and assigns the corresponding method 
        if (PlayerInfoScript.player == "Player")
        {
            CanUseAbility = CanKeyboardPlayerUseAbility; 
        }
        else
        {
            CanUseAbility = CanControllerPlayerUseAbility;
        }

        // Checks which player this script is attached to and assigns the corresponding method 
        if (abilityChoice == "Dash")
        {
            cooldown = 3f;
            UseAbilityMethod = Dash;
        }
        else
        {
            cooldown = 5f;
            UseAbilityMethod = Grenade;
        }
    } 
    void Update()
    {
        if (CanUseAbility()) // Checks if the player is able to use their ability
        {
            UseAbilityMethod(); // Activates the player's corresponding ability method
        }
    }
}
