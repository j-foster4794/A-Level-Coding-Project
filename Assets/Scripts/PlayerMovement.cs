using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{

    public int playerId; // Stores which player object this script is attached to, either 1 or 2

    public CharacterController controller;

    string horizontalaxis; // declaring an empty varible which will be used to store the movement of the horizontal axis
    string verticalaxis; // declaring an empty varible which will be used to store the movement of the vertical axis
    string Jump; 
    string Sprint;
    string Crouch; // Declaring an empty variable which will be used to stored the identifier of the crouch button

    [SerializeField] private float speed; // Value used to control the rate at which the player moves

    [SerializeField] private float basespeed = 4f; // Value used to control the base rate at which the player moves

    [SerializeField] private Vector3 velocity; // Stores the players current vertical velocity

    public bool grounded; // Variable which will be used to decide whether or not the player is touching the ground

    [SerializeField] private Transform groundcheck; // Holds the positional information about the ground check game object

    [SerializeField] private float groundcheckradius = 0.4f; // Stores the radius of the sphere created around the ground check 

    [SerializeField] private LayerMask groundlayer; // Stores the ground layer-type

    [SerializeField] private float gravity = -40f; // Stores the value of gravity to be used

    [SerializeField] private float jumpheight = 1.75f; // How far the player will jump

    [SerializeField] private float maxStamina = 25f; // The maximum stamina the player can have

    [SerializeField] private float stamina; // The current stamina the player has

    [SerializeField] private float staminaIncrease = 1.5f; // The rate at which the stamina increases when not sprinting

    [SerializeField] private float staminaDrain = 5f; // The rate at which the stamina decreases when sprinting

    [SerializeField] private float speedModifier = 2.25f; // Stores the multiplier the base speed should be increased by

    [SerializeField] private bool crouching; // boolean to store whether the player is crouching

    [SerializeField] private Transform Player; // Creates a variable to store the transform of the player
    
    [SerializeField] private float crouchingSpeed = 3f;

    [SerializeField] private bool sprinting;


    public void Awake() // Called at the creation of the object to configure the initial conditions depending on the player
    {
        stamina = maxStamina; // Sets the stamina to the maximum value it can be
        speed = basespeed; // sets the player speed to the base speed
        if (playerId == 1) // if it is player 1, then the keyboard must be used 
        {
            horizontalaxis = "HorizontalKeyboard"; // Stores the Horizontal Keyboard axis name in this variable, meaning that the horizontalaxis variable can be generic and used for both players
            verticalaxis = "VerticalKeyboard"; // Stores the Vertical Keyboard axis name in this variable, meaning that the horizontalaxis variable can be generic and used for both players
            Jump = "KeyboardJump";
            Sprint = "KeyboardSprint";
            Crouch = "KeyboardCrouch";
        }
        else if (playerId == 2) // if it is player 2, then the controller must be used
        {
            horizontalaxis = "HorizontalJoystick"; // Stores the horizontal controller joystick axis name in this variable, meaning that the horizontalaxis variable can be generic and used for both players
            verticalaxis = "VerticalJoystick"; // Stores the Vertical controller joystick axis name in this variable, meaning that the verticalaxis variable can be generic and used for both players
            Jump = "ControllerJump";
            Sprint = "ControllerSprint";
            Crouch = "ControllerCrouch";
        }
    }
    void Update()
    {
        float x = Input.GetAxis(horizontalaxis); // returns the horizontal axis movement of the players input device and stores it in this float variable 
        float z = Input.GetAxis(verticalaxis); // returns the vertical axis movement of the players input device and stores it in this float variable
        Vector3 move = (transform.right * x + transform.forward *z).normalized; // Creates a normalised resultant vector3 object from the sum of the products of the player's forward facing and sidefacing vectors and the axis input
        controller.Move(move * Time.deltaTime * speed); // Moves the player by the vector3 which is normalised by the value of the time since last frame. 

        grounded = Physics.CheckSphere(groundcheck.position,groundcheckradius,groundlayer); // Casts a sphere at the base of the player, if it collides with the ground then it returns true, the returned value is stored in the variable

        if (grounded && velocity.y < 0) // If the player is both on the ground and the velocity is less than 0, it sets the velocity to -1 to prevent the velocity decreasing indefinitely
        {
            velocity.y = -1; 
        }        

        if (grounded && Input.GetButton(Jump) && !crouching) // if the player is on the ground and the jump button is pressed and not crouching, then it executes the jump
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2 * gravity); // The vector3 y-component is set to an initial jump velocity 
        }

        velocity.y = velocity.y + Time.deltaTime * gravity; // decreases the vector3 y-component based on a gravity value which has been adjusted so it is independent of frame rate

        controller.Move(velocity*Time.deltaTime); // Moves the character controller using the velocity vector3 which has been adjusted so it is independent of frame rate

        
        if (Input.GetButton(Sprint) && stamina > 0 && !crouching) // Checks if the player is wanting to sprint and is able to  
        {
            sprinting = true;
            speed = basespeed * speedModifier; // Sets the speed to an increased value
            stamina -= staminaDrain * Time.deltaTime; // Decreases the stamina a normalised amount so it is independent of framerate 
        }
        else
        {
            stamina += staminaIncrease * Time.deltaTime; // Increases the stamina a normalised amount so it is independent of framerate
        }

        if (Input.GetButtonUp(Sprint) || (stamina < 0) )  // Checks if the player releases the sprint button or runs out of stamina
        {
            sprinting = false; // Stops the player sprinting
            speed = basespeed;
        }
        if (stamina > maxStamina) // Checks if the stamina value is above its maximum value
        {
            stamina = maxStamina; // Sets the stamina value so it is at the cap.
        } 

        if (Input.GetButtonDown(Crouch) && grounded && !sprinting) // If the player is on the ground, not sprinting and presses the crouch button they crouch
        {
            crouching = true; 
            speed = crouchingSpeed; // Reduces the player's speed
            Player.localPosition += new Vector3(0f,-0.25f, 0f); // Reduces the position of the player
            Player.localScale = new Vector3(Player.localScale.x,0.7f,Player.localScale.z);  // Decreases the scale of the player along the y-axis
        }
        if (Input.GetButtonUp(Crouch)) // Checks if the player releases crouch
        {
            crouching = false;
            speed = basespeed; // Resets the player's speed
            Player.localPosition += new Vector3(0f,0.25f,0f); // Increases the position of the player
            Player.localScale = new Vector3(Player.localScale.x,1f,Player.localScale.z); // Resets the scale of the player along the y-axis
        }
    }
}