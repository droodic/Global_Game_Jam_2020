using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private bool hasDebrisBomb;

    public bool HasDebrisBomb { get => hasDebrisBomb; set => hasDebrisBomb = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(Input.GetKeyDown)
    }

    void ShootDebrisBomb()
    {
        if (hasDebrisBomb)
        {

        }
    }
}
