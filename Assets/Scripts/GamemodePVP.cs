using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamemodePVP : MonoBehaviour
{
    // Declaring all varaibles to be used, using the [SerializeField] means they are private but can be accessed in the unity editor
    [SerializeField] int player1Wins;
    [SerializeField] int player2Wins;
    [SerializeField] int roundCounter;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject winnerText;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject player1HealthDisplay;
    [SerializeField] GameObject player2HealthDisplay;
    [SerializeField] GameObject player1CooldownDisplay;
    [SerializeField] GameObject player2CooldownDisplay;
    [SerializeField] GameObject player1WinsDisplay;
    [SerializeField] GameObject player2WinsDisplay;

    void Awake()
    {
        // Initialising the gamemode tracking variables
        player1Wins = 0;
        player2Wins = 0;
        roundCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Fetches the script components which contain data for each player
        var player1Info = player1.GetComponent<PlayerInfo>();
        var player2Info = player2.GetComponent<PlayerInfo>(); 
        var player1AbilityScript = player1.GetComponent<ActivateAbility>();
        var player2AbilityScript = player2.GetComponent<ActivateAbility>();
        if (roundCounter != 5) // Checks that the game has not ended yet
        {
            // Updates the HUD health values for each player
            player1HealthDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(player1Info.health);
            player2HealthDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(player2Info.health);

            // Calculates the time left until the ability is able to be used and updates this value on the player HUD for each player
            float timeUntilNextCooldown = player1AbilityScript.cooldown - (Time.time - player1AbilityScript.lastAbilityUse);
            if (timeUntilNextCooldown <= 0)
            {
                timeUntilNextCooldown = 0;
            }
            player1CooldownDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(timeUntilNextCooldown));

            timeUntilNextCooldown = player2AbilityScript.cooldown - (Time.time - player2AbilityScript.lastAbilityUse);
            if (timeUntilNextCooldown <= 0)
            {
                timeUntilNextCooldown = 0;
            }
            player2CooldownDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(timeUntilNextCooldown));

            // Updates the score values for both players 
            player1WinsDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(player1Wins);
            player2WinsDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(player2Wins);

            if (player1Info.health <= 0) // Checks if player one has died, if so increases the round number and player 2's score
            {
                // Increments another round
                player2Wins++;
                roundCounter++;

                // Resets the player's health and position
                player1Info.health = player1Info.maxHealth;
                player2Info.health = player2Info.maxHealth;
                player1.GetComponent<CharacterController>().enabled = false;
                player2.GetComponent<CharacterController>().enabled = false;
                player1.transform.position = new Vector3(-12f,0.8f,-20f); 
                player2.transform.position = new Vector3(3f,0.8f,13f);
                player1.GetComponent<CharacterController>().enabled = true;
                player2.GetComponent<CharacterController>().enabled = true;
            }
            else if (player2Info.health <= 0) // Checks if player two has died, if so increases the round number and player 1's score
            {
                // Increments another round
                player1Wins++; 
                roundCounter++;

                // Resets the players' health and position
                player1Info.health = player1Info.maxHealth;
                player2Info.health = player2Info.maxHealth;
                player1.GetComponent<CharacterController>().enabled = false;
                player2.GetComponent<CharacterController>().enabled = false;
                player1.transform.position = new Vector3(-12f,0.8f,-20f); 
                player2.transform.position = new Vector3(3f,0.8f,13f);
                player1.GetComponent<CharacterController>().enabled = true;
                player2.GetComponent<CharacterController>().enabled = true;
            }
        }
        else
        {
            // Decides winner of the game and updates the text to display it
            if (player1Wins > player2Wins)
            {
                winnerText.GetComponent<TextMeshProUGUI>().text = "Player 1";
            }
            else
            {
                winnerText.GetComponent<TextMeshProUGUI>().text = "Player 2";
            }

            // Enables the end screen
            endScreen.gameObject.SetActive(true);

            // Checks if the player presses the space button down
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Loads the menu, unlocks the cursor and resets a menu counter
                SceneManager.LoadScene("Menu");
                Cursor.lockState = CursorLockMode.None;
                MainManager.Instance.numOfTimesGoneThroughMenu = 0;
            }
        }
    }
}