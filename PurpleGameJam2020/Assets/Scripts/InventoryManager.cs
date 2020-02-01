using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int maxInventory = 300;
    private int debrisCount = 50;

    public void Start()
    {
        Debug.Log(debrisCount);
    }

    public void AddDebrisCount(GameObject debris)
    {
        if (!hasReachedMaxInventory())
        {
            debrisCount++;
            Debug.Log(debrisCount);
            if (debris.transform.position == gameObject.transform.position)
            {
                Destroy(debris);
            }
        }
    }

    public bool hasReachedMaxInventory()
    {
        if (debrisCount >= maxInventory)
        {
            //Debug.Log("inventoryFull");
            return true;
        }
        else
        {
            //Debug.Log("inventoryNotFull");
            return false;
        }
    }

    public bool hasAnyDebris()
    {
        if(debrisCount != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveDebrisCount()
    {
        if (debrisCount > 0)
        {
            debrisCount--;
        }
        Debug.Log($"Inv: {debrisCount}");
    }
}
