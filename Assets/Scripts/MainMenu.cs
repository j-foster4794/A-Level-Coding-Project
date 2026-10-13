using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   public void Quit() // declares a method which can be called anywhere which enables the quit button to call it
   {
      Application.Quit(); // Quits the game
   }
}