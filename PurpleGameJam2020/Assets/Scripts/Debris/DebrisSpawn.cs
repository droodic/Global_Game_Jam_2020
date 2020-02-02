using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawn : MonoBehaviour
{
    [SerializeField] int minSpawn = 100;
    [SerializeField] int maxSpawn = 200;
    [SerializeField] int spawnLimit = 600;
    [SerializeField] GameObject debris;
    [SerializeField] GameObject powerUpBattery;

    [SerializeField] BoxCollider floorBounds;

    int spawnedDebris = 0;

    public int SpawnedDebris { get => spawnedDebris; set => spawnedDebris = value; }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDebris", 4f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnDebris()
    {
        if(SpawnedDebris < 800)
        {
            int rand;
            rand = Random.Range(minSpawn, maxSpawn);
            for (int i = 0; i < rand; i++)
            {
                Instantiate(debris, RandomPointInBounds(floorBounds.bounds), this.transform.rotation);
                SpawnedDebris++;
            }

            int powerUpRand;
            powerUpRand = Random.Range(1, 8);
            if(powerUpRand == 1)
            {
                Instantiate(powerUpBattery, RandomPointInBounds(floorBounds.bounds), this.transform.rotation);
            }
        }

    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y+2f, bounds.max.y+2f),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
