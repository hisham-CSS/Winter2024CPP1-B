using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject[] itemsToSpawn;
    [SerializeField] int itemSpawnWeight = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        int randNum = Random.Range(0, itemsToSpawn.Length + itemSpawnWeight);

        if (randNum > 2) return;

        Instantiate(itemsToSpawn[randNum], transform.position, Quaternion.identity);
    }
}
