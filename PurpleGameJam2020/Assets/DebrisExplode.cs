using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisExplode : MonoBehaviour
{
    [SerializeField] GameObject debris; // will need to assign this to player somehow
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RainDebris", 0.0235f, 0.0235f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RainDebris()
    {
        if(count < 235)
        {
           // Instantiate(debris, this.transform);
            count++;
        }
        else
        {
            CancelInvoke();
        }
    }
}
