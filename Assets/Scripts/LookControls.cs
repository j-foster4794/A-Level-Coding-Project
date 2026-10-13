using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookControls : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement; // Declares a variable to hold the game object associated with the playerMovement script, it is done in order to reference the playerId stored there. 
    [SerializeField] Transform player; // Declares a variable to hold the Transform game object component of the player, done in order to rotate the player


    string xAxis; // declaring empty variable which will store the name of the x axis to be reference later
    string yAxis; // declaring empty variable which will store the name of the y axis to be reference later

    float x; // declaring a float variable which will store the amount the character will be rotated in the x direction
    float y; // declaring a float variable which will store the amount the character will be rotated in the y direction

    float xRotation = 0; // declares a float variable to keep track of the rotation of the camera around the local x axis and assigns it to 0 as no rotation occurs to begin with

    [SerializeField] float sensitivity; // Declares an empty sensitivity varible used to determine how fast the screen should move to look at different things

    void Awake() // Called when the object is created 
    {
        if (playerMovement.playerId == 1) // if it is player 1, then the mouse should control the looking around controls
        {
            xAxis = "Mouse X"; // Assigns the name of corresponding axis to the variable for later reference
            yAxis = "Mouse Y"; // Assigns the name of corresponding axis to the variable for later reference
            sensitivity = 1.2f;
        }
        else if (playerMovement.playerId == 2) // if it is player 2, then the right joystick should control the look around controls
        {
            xAxis = "HorizontalJoystick2"; // Assigns the name of corresponding axis to the variable for later reference
            yAxis = "VerticalJoystick2"; // Assigns the name of corresponding axis to the variable for later reference
            sensitivity = 1.4f;
        }
        Cursor.lockState = CursorLockMode.Locked; // Locks the position of the mouse so it cannot be moved
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis(xAxis); // Returns to movement of input device along the xAxis, stores it to the variable to be used
        y = Input.GetAxis(yAxis);  // Returns to movement of input device along the yAxis, stores it to the variable to be used

        xRotation -= y * sensitivity; // Adjusts the rotation of the camera around the x axis

        xRotation = Mathf.Clamp(xRotation,-90f,90f);   // Limits the rotation of the camera to prevent it from rotating further than straight up or down

        player.Rotate(new Vector3(0,1,0) * x * sensitivity); // Applies a rotation of a certain amount of degrees given by the magnitude of the vector3 around the Y axis
        
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f); // Sets the local rotation of the camera 

    }
}