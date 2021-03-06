﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairObjects : MonoBehaviour
{
    Player player;
    private bool isRepairing = false;
    private RepairableBehaviour objectToRepair;
    [SerializeField] private GameObject debrisParticle;
    [SerializeField] private Collider _collider;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Update()
    {
        if (isRepairing && objectToRepair != null)
        {
            if (player.InputManager.Repairing && gameObject.GetComponent<InventoryManager>().hasAnyDebris() && objectToRepair.IsBroken)
            {
                objectToRepair.Repair(player);
                gameObject.GetComponent<InventoryManager>().RemoveDebrisCount();
                ShowParticles();
                var lookAt = objectToRepair.transform.position;
                //lookAt.y = 0.0f;
                player.Movement.Arm.transform.LookAt(lookAt);
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("RepairableObject") && !collider.GetComponentInParent<RepairableBehaviour>().IsRepaired())
        {
            objectToRepair = collider.GetComponentInParent<RepairableBehaviour>();
            isRepairing = true;
        }       
    }

    //public void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.CompareTag("RepairableObject") 
    //}

    void OnTriggerExit(Collider col)
    {
        isRepairing = false;
        player.Movement.Arm.transform.rotation = player.Movement.Body.transform.rotation;
    }

    public void ShowParticles()
    {
        var randomPosition = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f));
        var debrisClone = Instantiate(debrisParticle, (gameObject.transform.position + randomPosition + (Vector3.up * 0.2f)), Quaternion.identity, null);
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

            if (debris == null)
            {
                break;
            }
        }

    }
}
