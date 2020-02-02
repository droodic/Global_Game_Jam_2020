using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairObjects : MonoBehaviour
{
    Player player;
    private bool isRepairing = false;
    private RepairableBehaviour objectToRepair;
    [SerializeField] private GameObject debrisParticle;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Update()
    {
        if (isRepairing && objectToRepair != null)
        {
            if (Mouse.current.leftButton.ReadValue() > 0 && gameObject.GetComponent<InventoryManager>().hasAnyDebris() &&objectToRepair.IsBroken)
            {
                objectToRepair.Repair(player);
                gameObject.GetComponent<InventoryManager>().RemoveDebrisCount();
                ShowParticles();
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

    void OnTriggerExit(Collider col)
    {
        isRepairing = false;
    }

    public void ShowParticles()
    {
        var randomPosition = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f));
        var debrisClone = Instantiate(debrisParticle, (gameObject.transform.position + randomPosition + (Vector3.up * 0.2f)), Quaternion.identity, gameObject.transform);
        //debrisClone.transform.position = Vector3.MoveTowards(debrisClone.transform.position, objectToRepair.gameObject.transform.position, 10 * Time.deltaTime);
        StartCoroutine(MoveDebris(debrisClone));
        Destroy(debrisClone.gameObject, 1.0f);
    }

    IEnumerator MoveDebris(GameObject debris)
    {
        var randomPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        while (true)
        {
            debris.transform.position = Vector3.MoveTowards(debris.transform.position, (objectToRepair.gameObject.transform.position + randomPosition), 15 * Time.deltaTime);
            yield return null;

            if(debris == null)
            {
                break;
            }
        }

    }
}
