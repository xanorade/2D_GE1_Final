using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // the data defining the weapon's stats
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;

    // the current cooldown for the weapon
    float currentCooldown;

    // reference to the player movement script
    protected PlayerMovement pm;

    // start is called before the first frame update
    protected virtual void Start()
    {
        // find and assign the player movement script
        pm = FindObjectOfType<PlayerMovement>();

        // set the current cooldown to be the cooldown duration at the start
        currentCooldown = weaponData.CooldownDuration;
    }

    // update is called once per frame
    protected virtual void Update()
    {
        // reduce the current cooldown over time
        currentCooldown -= Time.deltaTime;

        // if the cooldown has elapsed, trigger an attack
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    // method to be overridden by child classes to define the attack behavior
    protected virtual void Attack()
    {
        // reset the cooldown to the weapon's cooldown duration
        currentCooldown = weaponData.CooldownDuration;
    }

    // method to get the mouse position in the world space
    protected Vector3 GetMouseWorldPosition()
    {
        // get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // set the z-coordinate to the near clip plane of the camera
        mouseScreenPosition.z = Camera.main.nearClipPlane;

        // convert the mouse position from screen space to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // set the z-coordinate to 0 to ensure it's on the same plane as the game objects
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
