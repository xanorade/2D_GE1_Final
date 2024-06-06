using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    AudioManager audioManager;

    // current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;

    void Awake()
    {
        // assigning the initial values from the EnemyScriptableObject
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        // finding the player GameObject and assign its transform to the player variable
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update()
    {
        // checking if the distance between the enemy and the player is greater than the despawn distance
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            // if so, return the enemy to the spawn position
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        // reducing the current health of the enemy by the damage amount
        currentHealth -= dmg;
        audioManager.PlaySFX(audioManager.damageGiven);
        // checking if the enemy's health is zero or less
        if (currentHealth <= 0)
        {
            // if so, destroy the enemy
            Kill();
        }
    }

    public void Kill()
    {
        // destroying the enemy GameObject
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // checking if the enemy collides with the player
        if (col.gameObject.CompareTag("Player"))
        {
            // if so, get the PlayerStats component from the player GameObject and apply damage
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        // when the enemy is destroyed, inform the EnemySpawner that an enemy has been killed
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        // return the enemy to a random spawn point relative to the player's position
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
