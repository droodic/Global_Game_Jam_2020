using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    /// <summary>
    /// Debris collection behaviour
    /// </summary>
    /// <param name="collidier"></param>
    public void OnTriggerEnter(Collider collidier)
    {
        if (collidier.gameObject.tag == "Player") //to change to vacuum part
        {
            var ivm = collidier.gameObject.GetComponent<InventoryManager>();
            ivm.AddDebrisCount(this.gameObject);
        }
    }
}
