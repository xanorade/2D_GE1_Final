using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // finds the EnemyStats component in the scene and assign it to the enemy variable
        enemy = FindObjectOfType<EnemyStats>();
        // finds the PlayerMovement component in the scene and assign its transform to the player variable
        player = FindObjectOfType<PlayerMovement>().transform;
        // gets the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // moves the enemy towards the player's position with a speed determined by enemy.currentMoveSpeed
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);

        // checks if the player is to the left or right of the enemy and flip the sprite accordingly
        if (player.position.x < transform.position.x)
        {
            // flips the sprite horizontally if the player is to the left
            spriteRenderer.flipX = true;
        }
        else
        {
            // flips the sprite horizontally if the player is to the right
            spriteRenderer.flipX = false;
        }
    }
}
