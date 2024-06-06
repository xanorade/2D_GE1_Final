using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // list of terrain chunk prefabs
    public List<GameObject> terrainChunks;

    // reference to the player object
    public GameObject player;

    // radius for checking terrain around the player
    public float checkerRadius;

    // layer mask for terrain detection
    public LayerMask terrainMask;

    // current chunk where the player is located
    public GameObject currentChunk;

    // player's last position for movement detection
    Vector3 playerLastPosition;

    [Header("Optimization")]
    // list of spawned chunks
    public List<GameObject> spawnedChunks;

    // reference to the latest spawned chunk
    GameObject latestChunk;

    // maximum optimization distance from the player
    public float maxOpDist;

    // current optimization distance
    float opDist;

    // cooldown duration for optimization
    float optimizerCooldown;

    // optimization cooldown duration
    public float optimizationCooldownDur;

    void Start()
    {
        // initialize player's last position
        playerLastPosition = player.transform.position;
    }

    void Update()
    {
        // check for new chunks around the player and optimize existing chunks
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        // return if there is no current chunk
        if (!currentChunk)
        {
            return;
        }

        // calculate player's movement direction
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        // get the name of the movement direction
        string directionName = GetDirectionName(moveDir);

        // check and spawn new chunks in adjacent directions
        CheckAndSpawnChunk(directionName);

        // check and spawn additional chunks when moving diagonally
        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }

    }

    void CheckAndSpawnChunk(string direction)
    {
        // check if there is no terrain chunk at the specified position
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            // spawn a new chunk at the specified position
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        // determine the name of the movement direction based on the normalized direction vector
        direction = direction.normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        // spawn a random terrain chunk at the specified position
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        // decrement optimization cooldown
        optimizerCooldown -= Time.deltaTime;

        // return if optimization cooldown is not finished
        if (optimizerCooldown > 0f)
        {
            return;
        }

        // reset optimization cooldown
        optimizerCooldown = optimizationCooldownDur;

        // iterate through all spawned chunks and optimize them based on player's distance
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            chunk.SetActive(opDist <= maxOpDist);
        }
    }
}
