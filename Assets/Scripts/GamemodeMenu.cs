using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeMenu : MonoBehaviour
{   
    public void PvpChosen() // This method is  called when the 2 player button is clicked, this method will alter the value held in the MainManager gamemode choice to be "PVP"
    {
        MainManager.Instance.gamemodechoice = "PVP";
    }

    public void PveChosen() // This method is  called when the single button is clicked, this method will alter the value held in the MainManager gamemode choice to be "PVE"
    {
        MainManager.Instance.gamemodechoice = "PVE";
    }
}