﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    [SerializeField] GameObject debrisBomb;
    [SerializeField] SphereCollider sphere;
    [SerializeField] GameObject forceFieldPlayer1;
    [SerializeField] GameObject forceFieldPlayer2;

    Player player;
    UIManager ui;
    SphereCollider debrisSphere;

    float powerTimer = 5f;

    bool hasPowerUp;
    bool hasDebrisBomb;
    bool hasSpeedUp;
    bool hasMagnet;
    bool hasForceField;

    public bool HasDebrisBomb { get => hasDebrisBomb; set => hasDebrisBomb = value; }
    public bool HasSpeedUp { get => hasSpeedUp; set => hasSpeedUp = value; }
    public bool HasMagnet { get => hasMagnet; set => hasMagnet = value; }
    public bool HasPowerUp { get => hasPowerUp; set => hasPowerUp = value; }
    public bool HasForceField { get => hasForceField; set => hasForceField = value; }


    // Start is called before the first frame update
    void Start()
    {

        ui = FindObjectOfType<UIManager>();
        player = GetComponent<Player>();
        //debrisSphere = GetComponent<SphereCollider>();
    }

    public void UsePower()
    {
        if (HasPowerUp)
        {
            if (hasDebrisBomb) //1
            {
                GameObject bullet = Instantiate(debrisBomb, transform.forward, Quaternion.identity) as GameObject; //use arm forward
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                Debug.LogError("threw debris bomb");
                hasDebrisBomb = false;
                ui.UpdatePowerUI(player, 1, false);
            }
            else if (HasSpeedUp) //2
            {
                Debug.LogError("used speedup");
                player.SprintBuffed = true;
                HasSpeedUp = false;
                ui.UpdatePowerUI(player, 2, false);
                StartCoroutine(CancelPowers(2, 5f));
            }
            else if (hasMagnet) //3
            {
                Debug.LogError("used magnet power");
                ui.UpdatePowerUI(player, 3, false);
                sphere.radius = 6f;
                StartCoroutine(CancelPowers(3, 5f));
            }
            else if (hasForceField) //4
            {
                Debug.LogError("spawned force field");
                ui.UpdatePowerUI(player, 4, false);
                if (player == PlayerManager.Instance.Players[0])
                {
                    Destroy(Instantiate(forceFieldPlayer1, this.transform.position, forceFieldPlayer1.transform.rotation,  null).gameObject, 5f);
                }
                else if (player == PlayerManager.Instance.Players[1])
                {
                    Destroy(Instantiate(forceFieldPlayer2, this.transform.position, forceFieldPlayer2.transform.rotation, null).gameObject, 5f);
                }
                StartCoroutine(CancelPowers(4, 5f));
            }
            HasPowerUp = false;
        }

        else
        {
            Debug.LogError("No current power");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Power" && !HasPowerUp)
        {
            Destroy(col.gameObject);
            RollRandomPower();
            HasPowerUp = true;
            Debug.Log("collide with power");
        }
    }

    void RollRandomPower()
    {

        int num = 4;
        //num = Random.Range(1, 2);
        if (num == 1)
        {
            HasDebrisBomb = true;
        }
        else if (num == 2)
        {
            HasSpeedUp = true;
        }
        else if (num == 3)
        {
            HasMagnet = true;
        }
        else if(num == 4)
        {
            HasForceField = true;
        }

        ui.UpdatePowerUI(player);
    }


    IEnumerator CancelPowers(int powerNum, float sec)
    {
        yield return new WaitForSeconds(sec);
        if (powerNum == 2)
        {
            player.SprintBuffed = false;
            ui.UpdatePowerUI(player, 2, false);
            Debug.LogError("Coroutine end");
        }
        if (powerNum == 3)
        {
            hasMagnet = false;
            sphere.radius = 2f;
            Debug.LogError("Coroutine end");
        }
        if (powerNum == 4)
        {
            hasForceField = false;
            if (player == PlayerManager.Instance.Players[0])
            {
                Destroy(forceFieldPlayer1);
            }
            else if (player == PlayerManager.Instance.Players[1])
            {
                Destroy(forceFieldPlayer2);
            }
            Debug.LogError("Coroutine end");
        }
    }
}


