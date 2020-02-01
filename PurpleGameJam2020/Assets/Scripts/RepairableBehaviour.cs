using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableBehaviour : MonoBehaviour
{
    private bool isBroken = true;
    [SerializeField] private int debrisCountNeeded = 100;
    private int currentDebrisCount = 0;

    public void Awake()
    {
        isBroken = true;
    }

    public bool isRepaired()
    {
        if(currentDebrisCount >= debrisCountNeeded)
        {
            isBroken = false;
            return true;
        }
        return false;
    }

    public void Repair()
    {
        currentDebrisCount++;
    }

    public void Break()
    {
        isBroken = true;
        currentDebrisCount = 0;
    }
}
