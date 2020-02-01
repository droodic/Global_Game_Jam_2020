using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    /// <summary>
    /// On collection debris behaviour
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !(InventoryManager.Instance().hasReachedMaxInventory()))
        {
            Destroy(gameObject);
            InventoryManager.Instance().AddDebrisCount();
        }
    }
}
