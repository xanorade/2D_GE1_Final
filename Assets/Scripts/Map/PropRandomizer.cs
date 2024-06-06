using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{

    // list of spawn points where props will be instantiated
    public List<GameObject> propSpawnPoints;

    // list of prop prefabs to choose from
    public List<GameObject> propPrefabs;

    void Start()
    {
        // spawn props when the scene starts
        SpawnProps();
    }

    //(AI)
    void SpawnProps()
    {
        // loop through each spawn point
        foreach (GameObject sp in propSpawnPoints)
        {
            // select a random prop prefab from the list
            int rand = Random.Range(0, propPrefabs.Count);

            // instantiate the selected prop prefab at the spawn point's position
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);

            // set the spawned prop as a child of the spawn point
            prop.transform.parent = sp.transform;
        }
    }
}
