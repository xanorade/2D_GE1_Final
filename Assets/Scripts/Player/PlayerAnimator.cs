using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimater : MonoBehaviour
{
    // reference to the Animator component
    Animator am;

    // reference to the PlayerMovement script
    PlayerMovement pm;

    // reference to the SpriteRenderer component
    SpriteRenderer sr;

    void Start()
    {
        // gets the Animator component
        am = GetComponent<Animator>();

        // gets the PlayerMovement component
        pm = GetComponent<PlayerMovement>();

        // gets the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // checks if the player is moving
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            // sets the "Move" parameter in the animator to true
            am.SetBool("Move", true);
        }
        else
        {
            // sets the "Move" parameter in the animator to false
            am.SetBool("Move", false);
        }

        // calls the method to check and update the sprite direction
        SpriteDirectionChecker();
    }

    // method to check and update the sprite direction based on movement
    void SpriteDirectionChecker()
    {
        // checks if the player is moving left
        if (pm.lastHorizontalVector < 0)
        {
            // flips the sprite horizontally
            sr.flipX = true;
        }
        else
        {
            // flips the sprite back to its original direction
            sr.flipX = false;
        }
    }
}
