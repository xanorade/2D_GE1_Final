using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(AI)
public class ChunkTrigger : MonoBehaviour
{
    // reference to the map controller
    MapController mc;

    // target map chunk
    public GameObject targetMap;

    void Start()
    {
        // finds and assigns the map controller component
        mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // checks if the player is colliding with the trigger
        if (col.CompareTag("Player"))
        {
            // assigns the target map chunk as the current chunk in the map controller
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // checks if the player exits the trigger
        if (col.CompareTag("Player"))
        {
            // if the current chunk is the target map, set it to null
            if (mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}
