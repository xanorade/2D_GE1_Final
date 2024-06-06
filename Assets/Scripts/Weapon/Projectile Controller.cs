using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : WeaponController
{
    AudioManager audioManager;

    private void Awake()
    {
        // finds and assigns the AudioManager component by its tag
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {
        base.Start(); // csall the base class start method
    }

    protected override void Attack()
    {
        base.Attack(); // call the base class attack method
        GameObject spawnedKnife = Instantiate(weaponData.Prefab); // instantiate the knife prefab
        spawnedKnife.transform.position = transform.position; // set the position of the spawned knife to be the same as this object (which is parented to the player)

        Vector3 targetPosition = GetMouseWorldPosition(); // get the mouse position in the game world
        Vector3 direction = (targetPosition - transform.position).normalized; // calculate the direction from player to mouse position
        spawnedKnife.GetComponent<KnifeBehaviour>().SetDirection(direction); // set the direction of the knife

        audioManager.PlaySFX(audioManager.projectile); // play the projectile sfx
    }
}
