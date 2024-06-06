using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    //(AI)
    // serializable class to define drop rates for different items
    [System.Serializable]
    public class Drops
    {
        public string name; // Name of the item
        public GameObject itemPrefab; // Prefab of the item
        public float dropRate; // Drop rate percentage
    }

    // List of drop rates for different items
    public List<Drops> drops;

    // Called when the object is destroyed
    void OnDestroy()
    {
        // Checks if the object's scene is loaded
        if (!gameObject.scene.isLoaded)
        {
            return; // If not, return without dropping any item
        }

        // Generates a random number between 0 and 100
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        // List to store possible drops based on drop rates
        List<Drops> possibleDrops = new List<Drops>();

        // Iterates through each drop rate
        foreach (Drops rate in drops)
        {
            // If the random number is less than or equal to the drop rate, add it to possible drops
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        // If there are possible drops
        if (possibleDrops.Count > 0)
        {
            // Randomly select one drop from the possible drops
            Drops drop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            // Instantiate the selected item at the object's position
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
