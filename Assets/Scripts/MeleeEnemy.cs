using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MeleeEnemy : MonoBehaviour
{
    // Declares variables to hold references to the player object and the enemy's rigid body
    GameObject player;
    new Rigidbody rigidbody;
    
    // Declares variables to hold the information about the enemy
    float speed = 3.7f;
    float damage = 40;
    float lastAttackTime = 0;
    public float health = 75;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) // Checks if the enemy has died
        {
            // Increases the score 
            var gamemodePVE = GameObject.Find("GameManager").GetComponent<GamemodePVE>();
            gamemodePVE.scoreEarned += 30f;
             
            var movementScript = GameObject.Find("Player").GetComponent<PlayerMovement>(); // Fetches the movement script from the player
            if (!movementScript.grounded) // Checks if the player is not grounded
            {
                gamemodePVE.scoreEarned += 30f; // Awards the player bonus score if not grounded
            }
            Destroy(gameObject); // Destroys the enemy
        }
        
        // Movement
        Vector3 direction = player.transform.position - transform.position; // Calculates the straight line direction from the enemy to the player
        direction[1] = 0; // Removes the y componenet of the direction
        direction = direction.normalized;  // Normalises the direction to have a magnitude of 1
        rigidbody.velocity = speed * direction; // Sets the velocity of the enemy to the direction multiplied by the speed
        
        // Melee attack
        if (Time.time - lastAttackTime > 0.5) // Checks how long since the last attack was used
        {
            lastAttackTime = Time.time; // sets the last attack time to now
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f); // Creates a sphere and returns a list of all the colliders inside the sphere
            foreach (Collider collider in colliders) // Iterates through each collider in the list
            {
                if (collider.tag == "PlayerBody") // Checks if the collider belongs to the player
                {
                    var PlayerInfoScript = player.GetComponent<PlayerInfo>();
                    PlayerInfoScript.health -= damage; // Deals damage to the player
                }
            }
        }
    }
}   