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
        InvokeRepeating("RainDebris", 0.0175f, 0.0175f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RainDebris()
    {
        if(count < 300)
        {
            Instantiate(debris, this.transform);
            count++;
        }
        else
        {
            CancelInvoke();
        }
    }
}
