using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    // Declares variables for the ranged enemy
    float timeBetweenShots = 1;
    public float health = 40;
    float damage = 30;
    float timeOfLastShot = 0;

    // Declares variables to store references to materials, the player and the bullet prefab
    GameObject Player;
    public Material material;
    [SerializeField] GameObject BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");  // Fetches and stores the player game object
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) // Checks if the enemy has died
        {
            var gamemodePVE = GameObject.Find("GameManager").GetComponent<GamemodePVE>();
            gamemodePVE.scoreEarned += 30f; // Awards score to the player
            var movementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
            if (!movementScript.grounded) // Checks if the player is not grounded and if so awards bonus score to the player
            {
                gamemodePVE.scoreEarned += 30f;
            }
            Destroy(gameObject); // Destoys the enemy object
        }
        transform.LookAt(Player.transform); // Rotates the enemy to always look at the player
        
        // Checks if it has been long enough since the last shot
        if (Time.time - timeOfLastShot > timeBetweenShots)
        {
            timeOfLastShot = Time.time; // Resets the time of the last shot to the current time
            GameObject bullet = Instantiate(BulletPrefab, transform.position + transform.forward * 0.6f, transform.rotation); // creates a bullet with the position and rotation of the enemy
            bullet.transform.LookAt(Player.transform); // Rotates the bullet to be facing the player
            var bulletScript = bullet.GetComponent<Bullet>(); // Fetches and stores the bullet script component
            bulletScript.Initialise("Ranged Enemy", 20f, damage, 1.5f); // Initialises the bullet with necessary data
            bullet.GetComponent<MeshRenderer>().material = material; // Changes the material on the bullet
        }
    }
}