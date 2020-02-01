using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairObjects : MonoBehaviour
{
    private bool isRepairing = false;
    private RepairableBehaviour objectToRepair;

    void Update()
    {
        if (isRepairing && objectToRepair != null)
        {
            if (Mouse.current.leftButton.ReadValue() > 0 && gameObject.GetComponent<InventoryManager>().hasAnyDebris())
            {
                objectToRepair.Repair();
                gameObject.GetComponent<InventoryManager>().RemoveDebrisCount();
            }
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("RepairableObject") && !collider.GetComponent<RepairableBehaviour>().isRepaired())
        {
            objectToRepair = collider.GetComponent<RepairableBehaviour>();
            isRepairing = true;
        }
    }
}
