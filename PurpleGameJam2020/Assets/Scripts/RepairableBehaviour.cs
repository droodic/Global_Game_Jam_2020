using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableBehaviour : MonoBehaviour
{


    private bool isBroken = true;
    [SerializeField] Transform jumpLocation;
    [SerializeField] private int debrisCountNeeded = 100;
    private int currentDebrisCount = 0;

    public bool IsBroken { get => isBroken; set => isBroken = value; }
    public Transform JumpLocation { get => jumpLocation; set => jumpLocation = value; }

    public void Awake()
    {
        IsBroken = true;
    }

    public bool isRepaired()
    {
        if (currentDebrisCount >= debrisCountNeeded)
        {
            IsBroken = false;
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
            UIManager.Instance.UpdateVp();
        }
        else
        {
            isRepaired();
            IsBroken = false;
            player.VictoryPoints += 100;
            UIManager.Instance.UpdateVp();
        }
        Debug.Log($"Needed: {currentDebrisCount}");
    }

    public void Break()
    {
        IsBroken = true;
        currentDebrisCount = 0;
    }
}
