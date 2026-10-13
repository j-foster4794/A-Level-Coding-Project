using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Declares the variables associated with the bullet
    [SerializeField] string owner;
    [SerializeField] float speed;
    [SerializeField] float baseDamage;
    [SerializeField] float damageMultiplier;

    Rigidbody rBody; // Declares a ribid body variable 

    void Awake() // Called when the object is created
    {
        rBody = gameObject.GetComponent<Rigidbody>(); // Fetches the ribid body component from the bullet game object and assigns it to this variable
        rBody.useGravity = false; // Disables gravity on the bullet
        Destroy(gameObject,5); // Sets object to be destroyed after 5 seconds
    }

    public void Initialise(string _owner, float _speed, float _baseDamage, float _damageMultiplier) // Declares a function to set values of the bullet
    {
        // Assigns the passed in data to the corresponding variable
        owner = _owner;
        speed = _speed;
        baseDamage = _baseDamage;
        damageMultiplier = _damageMultiplier;
        rBody.velocity = gameObject.transform.forward * speed; // Sets the velocity of the bullet to the direction its facing and multiplies this by speed
    }

    public void DamagePlayer(GameObject player, float damage) // Declares a method to damage the player, passes in the player object and damage as parameters
    { 
        var playerInfo = player.GetComponent<PlayerInfo>(); // Fetches the player info script so details about the player can be edited 
        playerInfo.health -= damage; // Damages the player 
    }

    public void DamageEnemy(GameObject enemy, float damage) // Declares a function to damage an enemy, passes in the enemy object the damage to be dealt
    { 
        
        if (enemy.GetComponent<MeleeEnemy>() != null) // Checks if the enemy is the melee enemy
        {
            var EnemyScript = enemy.GetComponent<MeleeEnemy>(); // Stores the melee enemy script so values can be changed
            EnemyScript.health -= damage; // Damages the enemy
        }
        else
        {
            var EnemyScript = enemy.GetComponent<RangedEnemy>(); // Stores the ranged enemy script so values can be changed
            EnemyScript.health -= damage; // Damages the enemy
        }
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,0.1f); // Creates a sphere at bullet's location and returns all colliders it interacts with
        foreach (Collider collider in colliders) // Iterates through every collider interacted with
        {
            // Determines what object the bullet collided with
            if (collider.tag == "Environment") 
            {
                Destroy(gameObject);
            }
            else if(collider.tag == "PlayerBody") 
            {
                if (!(collider.transform.parent.name == owner)) // Checks that the player body is not the player who created the bullet
                {
                    DamagePlayer(collider.transform.parent.gameObject, baseDamage);
                    Destroy(gameObject);
                }
            }
            else if(collider.tag == "PlayerHead") 
            {
                if (!(collider.transform.parent.name == owner)) // Checks that the player body is not the player who created the bullet
                {
                    DamagePlayer(collider.transform.parent.gameObject, baseDamage * damageMultiplier); 
                    Destroy(gameObject);
                }
            }
            else if(collider.tag == "EnemyBody") // 
            {
                DamageEnemy(collider.transform.parent.gameObject, baseDamage);
                Destroy(gameObject);
            }
            else if (collider.tag == "EnemyHead")
            {
                DamageEnemy(collider.transform.parent.gameObject, baseDamage * damageMultiplier);
                var gamemodePVE = GameObject.Find("GameManager").GetComponent<GamemodePVE>(); // Fetches the game mode script to give bonus score
                gamemodePVE.scoreEarned += 30f;
                Destroy(gameObject);
            }
        }
    }
}