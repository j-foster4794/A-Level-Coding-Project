using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamemodePVE : MonoBehaviour
{
    // Declaring variables to be used later
    float timeOfCreation;
    public float scoreEarned = 0;
    float totalScore = 0;
    float highestScore;
    int chanceOfEnemySpawn = 150;
    int turn = 0;
    bool running = true;

    // Declaring variables to store references for the UI, Enemy and player game objects
    GameObject player;
    [SerializeField] Canvas endScreen;
    [SerializeField] GameObject highscoreDisplay;
    [SerializeField] GameObject currentScoreDisplay;
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangedEnemyPrefab;
    [SerializeField] GameObject healthDisplay;
    [SerializeField] GameObject abilityCooldownDisplay;
    [SerializeField] GameObject scoreDisplay;
    [SerializeField] GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        // Initialises values 
        timeOfCreation = Time.time;
        player = GameObject.Find("Player");
        highestScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        // Game loop that continues until player dies
        if (running)
        {
            // Checks if player dies, if so stops spawning enemies
            if (player.GetComponent<PlayerInfo>().health <= 0)
            {
                running = false;
            }

            // Spawning enemies

            if (UnityEngine.Random.Range(1,chanceOfEnemySpawn) == 1) // If a randomly generated number is equal to 1 then an enemy is spawned
            {
                if (turn == 0) // Decides to spawn melee enemy
                {
                    turn = 1 - turn; // Reverses the turn variable so that it will spawn a ranged enemy next
                    Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-20,20),0f,UnityEngine.Random.Range(-20,20)); // Randomly generates a vector3 spawn position on ground level
                    Instantiate(meleeEnemyPrefab, spawnPosition, Quaternion.identity); // Instantiates the melee enemy atthis position with an empty rotation
                }
                else // Decides to spawn the ranged enemy
                {
                    turn = 1 - turn; // Reverses the turn variable so it will spawn a melee enemy next
                    Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range(-20,20), 5f, UnityEngine.Random.Range(-20,20)); // Randomly generates a vector3 spawn position far above the ground
                    Instantiate(rangedEnemyPrefab, spawnPosition, Quaternion.identity); // Instantiates the ranged enemy at this position with an empty rotation
                }
            }

            // Updating the score
            totalScore = scoreEarned + (Time.time - timeOfCreation);

            // Updating the HUD
            healthDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(player.GetComponent<PlayerInfo>().health);
            var abilityScript = player.GetComponent<ActivateAbility>(); // Fetches the ability script from the player object
            float timeUntilNextCooldown = abilityScript.cooldown - (Time.time - abilityScript.lastAbilityUse); // Calculates the time left until the ability is able to be used and updates this value on the player HUD for the player
            if (timeUntilNextCooldown <= 0)
            {
                timeUntilNextCooldown = 0;
            }
            abilityCooldownDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(timeUntilNextCooldown));
            scoreDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(totalScore)); // Updates the score on the HUD

            // Increase the difficulty at certain times
            if (Time.time - timeOfCreation > 90)
            {
                chanceOfEnemySpawn = 30;
            }
            else if(Time.time - timeOfCreation > 60)
            {
                chanceOfEnemySpawn =  60;
            }
            else if(Time.time - timeOfCreation > 30)
            {
                chanceOfEnemySpawn = 100;
            }
        }
        else
        {
            // Disables the HUD when the game is over
            HUD.gameObject.SetActive(false);
            // Deciding highest score
            if (totalScore > highestScore)
            {
                PlayerPrefs.SetInt("HighScore", Convert.ToInt32(totalScore)); // Sets the highest score 
                PlayerPrefs.Save(); // Saves all changes made 
            }
            // Enabling the end screen and displaying score values
            endScreen.gameObject.SetActive(true);
            highscoreDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(highestScore));
            currentScoreDisplay.GetComponent<TextMeshProUGUI>().text = Convert.ToString(Mathf.Round(totalScore));

            if (Input.GetKeyDown(KeyCode.Space)) // If the uses presses space the menu is loaded again, the cursor is unlocked and a menu counter is reset
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Menu");
                MainManager.Instance.numOfTimesGoneThroughMenu = 0;
            }
        }
    }
}