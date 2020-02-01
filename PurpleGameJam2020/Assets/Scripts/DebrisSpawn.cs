using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawn : MonoBehaviour
{
    [SerializeField] GameObject debrisOne;
    [SerializeField] GameObject debrisTwo;

    [SerializeField] MeshCollider floorBounds;

    int spawnedDebris = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDebris", 10f, 12.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnDebris()
    {
        if(spawnedDebris < 550)
        {
            int rand;
            rand = Random.Range(250, 300);
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
