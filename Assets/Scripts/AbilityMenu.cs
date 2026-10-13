using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityMenu : MonoBehaviour
{
    [SerializeField] GameObject WeaponMenu; // Allows this script to access the weapon menu game object once it is attached

    [SerializeField] GameObject Canvas; // Allows this script to access the canvas game object once it is attached

    private bool GoneThroughMenuBefore() // Declaring a function to determine whether the weapon and ability menu have been displayed before, enabling the methods to know which player the weapon choice should be set to
    {
        if(MainManager.Instance.numOfTimesGoneThroughMenu == 0)
        {  
            return false; 
        }
        else
        {
            return true;
        }
    }

    private void EndOfMenuDecision() // Function which determines whether to load the scene or to go through the menu once more
    {
        if (GoneThroughMenuBefore() & (MainManager.Instance.gamemodechoice == "PVP")) // If the menu has been gone through before and it is the PVP mode, then the game should start.
        {
            Canvas.SetActive(false); // Disables the entire canvas
            SceneManager.LoadScene("PVP-Scene"); // Loads the PVP scene
        }   
        else if(MainManager.Instance.gamemodechoice == "PVE") // If it is the PVE mode the game should start no matter what  
        {
            Canvas.SetActive(false); // Disables the entire canvas 
            SceneManager.LoadScene("PVE-Scene"); // Loads the PVP scene
        }
        else // If it is the PVP menu and has only been through once, then it needs to go through again 
        { 
            WeaponMenu.SetActive(true); // activates the Weapon Menu
            MainManager.Instance.numOfTimesGoneThroughMenu += 1; // Increments the number of times gone through the menu
        }
    }


    public void Dash() // This function determines which ability choice variable to assign the "Dash" string to  
    {
        // If the menu has been through before then player 2 is assigned the dash ability, otherwise player 1 is assigned it
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.abilitychoice2 = "Dash";
        }
        else
        {
            MainManager.Instance.abilitychoice1 = "Dash";
        }
        
        EndOfMenuDecision();
    }

    public void Grenade() // This function determines which ability choice variable to assign the "Grenade" string to  
    {
        // If the menu has been through before then player 2 is assigned the grenade ability, otherwise player 1 is assigned it
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.abilitychoice2 = "Grenade";
        }
        else
        {
            MainManager.Instance.abilitychoice1 = "Grenade";
        }

        EndOfMenuDecision();
    }
}