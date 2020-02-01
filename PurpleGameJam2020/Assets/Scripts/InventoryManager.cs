using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int maxInventory = 300;
    private int debrisCount = 0;

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

    public void RemoveDebrisCount()
    {
        debrisCount--;
    }
}
