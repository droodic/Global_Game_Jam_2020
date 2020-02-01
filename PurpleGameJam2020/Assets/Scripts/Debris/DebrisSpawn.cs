using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawn : MonoBehaviour
{
    [SerializeField] int minSpawn = 100;
    [SerializeField] int maxSpawn = 200;
    [SerializeField] int spawnLimit = 600;
    [SerializeField] GameObject debrisOne;
    [SerializeField] GameObject debrisTwo;

    [SerializeField] MeshCollider floorBounds;

    int spawnedDebris = 0;

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
        if(spawnedDebris < 800)
        {
            int rand;
            rand = Random.Range(minSpawn, maxSpawn);
            for (int i = 0; i < rand; i++)
            {
                Instantiate(debrisOne, RandomPointInBounds(floorBounds.bounds), this.transform.rotation);
                spawnedDebris++;
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
