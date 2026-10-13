using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Declaring variables
    [SerializeField] float timeOfCreation;
    [SerializeField] float explosionRadius;
    [SerializeField] float timeUntilExplosion;
    [SerializeField] float damage;
    [SerializeField] Rigidbody rbody;

    public void Initialise(Vector3 velocity) // Declares a method which assigns values to the declared variables and give the grenade velocity  
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        rbody.velocity = velocity;
        timeOfCreation = Time.time;
        explosionRadius = 10f;
        timeUntilExplosion = 0.75f;
        damage = 150f;
        rbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        if ((Time.time - timeOfCreation) > timeUntilExplosion) // Checks if it is time for the grenade to explode
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius); // Returns an array of all the colliders which collided with a sphere creates around the grenade object
            foreach (Collider collider in colliders) // Iterates through all of colliders which the sphere collided with
            {
                if (collider.tag == "PlayerBody") // Checks if the grenade explosion hit the player
                {
                    var PlayerInfoScript = collider.transform.parent.gameObject.GetComponent<PlayerInfo>(); // Fetches the player info script so values can be adjusted
                    PlayerInfoScript.health -= damage; // Deals damage to the player
                }
                else if (collider.tag == "EnemyBody") // Checks if the grenade explosion hit an enemy
                {
                    if (collider.transform.parent.gameObject.GetComponent<MeleeEnemy>() != null) // Checks if the melee enemy was hit
                    {
                        var EnemyInfoScript = collider.transform.parent.gameObject.GetComponent<MeleeEnemy>();
                        EnemyInfoScript.health -= damage; // Damages the melee enemy
                    }
                    else
                    {
                        var EnemyInfoScript = collider.transform.parent.gameObject.GetComponent<RangedEnemy>();
                        EnemyInfoScript.health -= damage; // Damages the ranged enemy
                    }        
  

                }
            }
            Destroy(gameObject); // Destroys the grenade game object once it has exploded
        }
        
    }
}