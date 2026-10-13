using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    private bool GoneThroughMenuBefore() // declaring a function to determine whether the weapon and ability menu have been displayed before, enabling the methods to know which player the weapon choice should be set to, It is declared private so it won't interfere with other methods
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

    public void Sniper() // This function determines which weaponchoice variable to assign the sniper string to and is called when the sniper button is pressed 
    {
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.weaponchoice2 = "Sniper";
        }
        else
        {
            MainManager.Instance.weaponchoice1 = "Sniper";
        }
    }

    public void Shotgun() // This function determines which weaponchoice variable to assign the shotgun string to and is called when the shotgun button is pressed 
    {
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.weaponchoice2 = "Shotgun";
        }
        else
        {
            MainManager.Instance.weaponchoice1 = "Shotgun";
        }
    }

    public void Pistol() // This function determines which weaponchoice variable to assign the Pistol string to and is called when the pistol button is pressed 
    {
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.weaponchoice2 = "Pistol";
        }
        else
        {
            MainManager.Instance.weaponchoice1 = "Pistol";
        }
    }

    public void ARifle() // This function determines which weaponchoice variable to assign the ARifle string to and is called when the ARifle button is pressed 
    {
        if (GoneThroughMenuBefore())
        {
            MainManager.Instance.weaponchoice2 = "ARifle";
        }
        else
        {
            MainManager.Instance.weaponchoice1 = "ARifle";
        }
    }
}