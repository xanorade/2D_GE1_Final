using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    AudioManager audioManager;

    private void Awake() 
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player

        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 direction = (targetPosition - transform.position).normalized;
        spawnedKnife.GetComponent<KnifeBehaviour>().SetDirection(direction);   //Reference and set the direction

        audioManager.PlaySFX(audioManager.projectile);
    }
}
