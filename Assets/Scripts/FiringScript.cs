using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript : MonoBehaviour
{
    // Declares variables 
    public string weaponChoice;
    public float damage;
    public float headshotMultiplier = 2f;
    public float speed;
    public string player;
    public float timeBetweenShots = 5f;
    public float timeOfShot;
    public bool triggerUsed = false;
    public float spawnOffSet = 0.9f;

    // Declares variables to store references to objects 
    public GameObject bulletPrefab; 
    public Camera playerCamera;

    // Declares delegate types to be used to store methods
    public delegate bool ShootChecks();
    ShootChecks CanPlayerShoot;

    public delegate void ShootMethods();
    ShootMethods ShootMethod;

    public bool CanKeyboardPlayerShoot() // Function which returns whether the keyboard player is able to shoot the gun
    {
        if (Input.GetMouseButtonDown(0) && ((Time.time - timeOfShot) > timeBetweenShots)) // Checks if they clicked the fire button and if its been long enough since last shot
        {
            timeOfShot = Time.time; // resets time of shot to current time
            return true; 
        }
        return false; 
    }

    public bool CanControllerPlayerShoot() // Function which returns whether controller player is able to shoot the gun
    {
        // Checks if the trigger is pressed, it has been long enough and the trigger is not in use
        if ((Input.GetAxisRaw("Shoot") < -0.5f) && ((Time.time - timeOfShot) > timeBetweenShots) && !triggerUsed)
        {
            timeOfShot = Time.time; // Resets time of shot to current time
            triggerUsed = true; // Sets the variable to declare that the trigger is being used
            return true; 
        }
        else if (Input.GetAxisRaw("Shoot") > -0.5f) // Checks if the trigger is not in use 
        {
            triggerUsed = false; // sets the varaible to declare that the trigger is not being used
        }
        return false; 
    }

    public void ShootPistol() // Procedure which creates a single bullet at a position and customises its values
    {
        GameObject bullet = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet, playerCamera.transform.rotation); 
        var BulletScript = bullet.GetComponent<Bullet>(); 
        BulletScript.Initialise(player, speed, damage, headshotMultiplier);
    }

    public void ShootARifle() // Procedure which creates a single bullet at a position and customises its values
    {
        GameObject bullet = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet, playerCamera.transform.rotation);
        var BulletScript = bullet.GetComponent<Bullet>();
        BulletScript.Initialise(player, speed, damage, headshotMultiplier);
    }

    public void ShootSniper() // Procedure which creates a single bullet at a position and customises its values
    {
        GameObject bullet = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet, playerCamera.transform.rotation);
        var BulletScript = bullet.GetComponent<Bullet>();
        BulletScript.Initialise(player, speed, damage, headshotMultiplier);
    }

    public void ShootShotgun() // Procedure which creates 5 bullet at offset positions and customises each of their values
    {
        GameObject bullet = Instantiate(bulletPrefab, playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet, playerCamera.transform.rotation);
        var BulletScript = bullet.GetComponent<Bullet>();
        BulletScript.Initialise(player, speed, damage, headshotMultiplier);

        GameObject bullet2 = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet + playerCamera.transform.right * 0.3f, playerCamera.transform.rotation);
        var BulletScript2 = bullet2.GetComponent<Bullet>();
        BulletScript2.Initialise(player, speed, damage, headshotMultiplier);

        GameObject bullet3 = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet - playerCamera.transform.right * 0.3f, playerCamera.transform.rotation);
        var BulletScript3 = bullet3.GetComponent<Bullet>();
        BulletScript3.Initialise(player, speed, damage, headshotMultiplier);

        GameObject bullet4 = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet + playerCamera.transform.up * 0.3f, playerCamera.transform.rotation);
        var BulletScript4 = bullet4.GetComponent<Bullet>();
        BulletScript4.Initialise(player, speed, damage, headshotMultiplier);

        GameObject bullet5 = Instantiate(bulletPrefab,playerCamera.transform.position + playerCamera.transform.forward * spawnOffSet - playerCamera.transform.up * 0.3f, playerCamera.transform.rotation);
        var BulletScript5 = bullet5.GetComponent<Bullet>();
        BulletScript5.Initialise(player, speed, damage, headshotMultiplier);
    }
    public void Initialise(string _weaponChoice, string _player) // Function which passes in the player choice variables to be stored and used
    {
        // Assigns the passed in values so they can be used
        weaponChoice = _weaponChoice;
        player = _player;

        speed = 80f;
        headshotMultiplier = 2f;

        if (player == "Player") // Checks if player is player 1
        {
            CanPlayerShoot = CanKeyboardPlayerShoot; // Assigns the check keyboard player can shoot method to the main check can shoot method
        }
        else // Player must be player 2
        {
            CanPlayerShoot = CanControllerPlayerShoot; // Assigns the check controller player can shoot method to the main check can method
        }

        //Adjusts values for the bullet depending on the different weapon selected
        if (weaponChoice == "Pistol")
        {
            damage = 75;
            timeBetweenShots = 0.5f;
            ShootMethod = ShootPistol;
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else if (weaponChoice == "ARifle")
        {
            damage = 25;
            timeBetweenShots = 0.1f;
            ShootMethod = ShootARifle;
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (weaponChoice == "Sniper")
        {
            damage = 100;
            timeBetweenShots = 1.5f;
            ShootMethod = ShootSniper;
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (weaponChoice == "Shotgun")
        {
            damage = 25;
            timeBetweenShots = 1f;
            ShootMethod = ShootShotgun;
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {   
        if (CanPlayerShoot()) // Checks if the player is able to shoot
        {
            ShootMethod(); // Shoots with the method corresponding to the player's choice
        }
    }
}