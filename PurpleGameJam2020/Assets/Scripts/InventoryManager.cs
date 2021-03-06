﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int maxInventory = 300;
    [Header("Debug")]
    [SerializeField] private int debrisCount = 0;

    DebrisSpawn ds;
    public int DebrisCount { get => debrisCount; set => debrisCount = value; }

    public void Start()
    {
        ds = FindObjectOfType<DebrisSpawn>();
        //Debug.Log(DebrisCount);
    }

    public void AddDebrisCount(GameObject debris)
    {
        if (!hasReachedMaxInventory())
        {
            DebrisCount++;
            ds.SpawnedDebris--;
           // Debug.Log(DebrisCount);
            /*if (debris.transform.position == gameObject.transform.position)
            {
                Destroy(debris);
            }*/
        }
    }

    public bool hasReachedMaxInventory()
    {
        if (DebrisCount >= maxInventory)
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
            UIManager.Instance.UpdateDebrisUI();
        }
      //  Debug.Log($"Inv: {debrisCount}");
    }
}
