using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))] // requires a CircleCollider2D component to be attached to the GameObject
public class PlayerCollector : MonoBehaviour
{
    // reference to the PlayerStats script
    PlayerStats player;

    // reference to the CircleCollider2D component
    CircleCollider2D detector;

    // speed at which the player collects pickups
    public float pullSpeed;

    void Start()
    {
        // gets the PlayerStats component from the parent GameObject
        player = GetComponentInParent<PlayerStats>();
    }

    // sets the radius of the collector
    public void SetRadius(float r)
    {
        // gets or adds the CircleCollider2D component
        if (!detector) detector = GetComponent<CircleCollider2D>();
        detector.radius = r;
    }

    // when something enters the trigger collider
    void OnTriggerEnter2D(Collider2D col)
    {
        // checks if the collided object has a Pickup component
        if (col.TryGetComponent(out Pickup p))
        {
            // collects the pickup by calling the Collect method of the Pickup component
            // passes the player reference and pull speed as parameters
            p.Collect(player, pullSpeed);
        }
    }
}