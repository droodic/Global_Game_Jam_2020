using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisExplode : MonoBehaviour
{
    [SerializeField] GameObject debris; // will need to assign this to player somehow

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Instantiate(debris, this.transform);
       // Destroy(gameObject);
    }
}
