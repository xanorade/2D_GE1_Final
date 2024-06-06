using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // class to define a wave of enemies
    [System.Serializable]
    public class Wave
    {
        public string waveName; // name of the wave
        public List<EnemyGroup> enemyGroups; // list of enemy groups in the wave
        public int waveQuota; // total number of enemies to spawn in the wave
        public float spawnInterval; // time interval between each enemy spawn
        public int spawnCount; // number of enemies spawned
    }

    // class to define a group of enemies
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName; // name of the enemy group
        public int enemyCount; // total number of enemies to spawn in the group
        public int spawnCount; // number of enemies spawned
        public GameObject enemyPrefab; // prefab of the enemy to spawn
    }

    public List<Wave> waves; // list of waves to spawn
    public int currentWaveCount; // current wave index

    [Header("spawner attributes")]
    float spawnTimer; // timer to track spawn intervals
    public int enemiesAlive; // number of currently alive enemies
    public int maxEnemiesAllowed; // maximum number of enemies allowed alive at once
    public bool maxEnemiesReached = false; // flag to indicate if maximum enemies are reached
    public float waveInterval; // time interval between waves
    bool isWaveActive = false; // indicates if a wave is currently active

    [Header("spawn positions")]
    public List<Transform> relativeSpawnPoints; // list of spawn positions relative to player

    Transform player; // reference to the player object

    
    void Start()
    {
        // find the player object in the scene
        player = FindObjectOfType<PlayerStats>().transform;
        // calculate the quota for the initial wave
        CalculateWaveQuota();
    }

  
    void Update()
    {
        // check if the conditions are met to begin the next wave
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave()); // begin the next wave
        }

        // update the spawn timer
        spawnTimer += Time.deltaTime;
        // check if it's time to spawn enemies
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f; // reset the spawn timer
            SpawnEnemies(); // spawn enemies
        }
    }

    // coroutine to begin the next wave after a delay (AI)
    IEnumerator BeginNextWave()
    {
        isWaveActive = true; // set wave active to true

        // wait for the specified wave interval
        yield return new WaitForSeconds(waveInterval);

        // checks if there are more waves to spawn
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false; // sets wave active flag to false
            currentWaveCount++; // moves to the next wave
            CalculateWaveQuota(); // calculates the quota for the new wave
        }
    }

    // method to calculate the total number of enemies to spawn in the current wave
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0; // initialize the quota counter
        // iterate through each enemy group in the current wave
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount; // add the enemy count of each group to the quota
        }

        // set the wave quota in the wave object
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota); // log the wave quota for debugging
    }

    // method to spawn enemies in the current wave
    void SpawnEnemies()
    {
        // checks if the maximum enemies are reached or the current wave quota is met
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            // iterate through each enemy group in the current wave
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // checks if the spawn count of the group is less than the total enemy count
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // instantiates the enemy prefab at a random spawn point relative to the player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    // increments the spawn count of the group and the current wave
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++; // increments the total enemies alive
                    // checks if the maximum enemies are reached
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true; // sets the max enemies reached flag to true
                        return; // exits the method
                    }
                }
            }
        }
    }
    // method called when an enemy is killed
    public void OnEnemyKilled()
    {
        enemiesAlive--; // decrements the total enemies alive
        // checks if the total enemies alive is less than the maximum allowed enemies
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false; // resets the max enemies reached flag
        }
    }
}
