﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairableBehaviour : MonoBehaviour
{
    private bool isBroken;
    [SerializeField] Transform jumpLocation;
    [SerializeField] private int vpCompleteBoost = 100;
    [SerializeField] private int debrisCountNeeded = 100;
    private int currentDebrisCount = 0;
    [SerializeField] private GameObject repairZones;
    [SerializeField] private TextMesh progressText;

    public bool IsBroken { get => isBroken; set => isBroken = value; }
    public Transform JumpLocation { get => jumpLocation; set => jumpLocation = value; }
    public float RepairedPercentage { get => currentDebrisCount / (float)debrisCountNeeded; }

    public void Start()
    {
        repairZones.SetActive(false);
        currentDebrisCount = debrisCountNeeded;
    }

    public void Update()
    {
        DisplayRepairSign();
        UpdateText();
    }

    public bool IsRepaired()
    {
        if (currentDebrisCount >= debrisCountNeeded)
        {
            this.isBroken = false;
            UpdateText();
            return true;
        }
        return false;
    }

    public void Repair(Player player)
    {
        if (currentDebrisCount <= debrisCountNeeded)
        {
            currentDebrisCount++;
            player.VictoryPoints++;
            if (IsRepaired())
            {
                player.VictoryPoints += vpCompleteBoost;
                UIManager.Instance.UpdateWithBonus(player, vpCompleteBoost);
            }
            UIManager.Instance.UpdateVp();
            UpdateText();
        }
        //else
        //{
        //    Debug.Log("HEY!!!!!!!");
        //    IsRepaired();
        //    this.isBroken = false;
        //    
        //    UIManager.Instance.UpdateVp();
        //    UIManager.Instance.UpdateWithBonus(player, vpCompleteBoost);
        //    UpdateText();
        //}
        //Debug.Log($"Needed: {currentDebrisCount}");
    }

    public void Break()
    {
        this.isBroken = true;
        currentDebrisCount = 0;
        UpdateText();
    }

    public void DisplayRepairSign()
    {
        if (repairZones != null)
        {

            if (this.isBroken)
            {
                repairZones.SetActive(true);
            }
            else
            {
                repairZones.SetActive(false);
            } 
        }
    }

    public void UpdateText()
    {
        progressText.text = (RepairedPercentage * 100).ToString("F0") + "%";
        //progressText.text = currentDebrisCount.ToString() + " / " + debrisCountNeeded.ToString();
    }
}
