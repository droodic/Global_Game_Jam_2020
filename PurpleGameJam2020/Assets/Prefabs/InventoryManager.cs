using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int maxInventory = 5;
    private int debrisCount = 0;

    private static InventoryManager _instance = null;
    public static InventoryManager Instance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<InventoryManager>();
        }
        return _instance;
    }

    public void Start()
    {
        Debug.Log(debrisCount);
    }

    public void AddDebrisCount()
    {
        debrisCount++;
        Debug.Log(debrisCount);
    }

    public bool hasReachedMaxInventory()
    {
        if(debrisCount >= maxInventory)
        {
            Debug.Log("inventoryFull");
            return true;
        }
        else
        {
            Debug.Log("inventoryNotFull");
            return false;
        }
    }
}
