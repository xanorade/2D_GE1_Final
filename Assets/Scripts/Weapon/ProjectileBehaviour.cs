using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start(); // calls the Start method of the base class 
    }

    void Update()
    {
        // sets the movement of the knife based on direction, speed, and time
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    // method to set the direction of the knife
    public void SetDirection(Vector3 dir)
    {
        direction = dir; // Set the direction to the provided vector
    }
}
