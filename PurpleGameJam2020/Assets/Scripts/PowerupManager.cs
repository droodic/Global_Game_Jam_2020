using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    Player player;
    UIManager ui;

    float powerTimer = 5f;
    bool hasPowerUp;
    bool hasDebrisBomb;
    bool hasSpeedUp;

    public bool HasDebrisBomb { get => hasDebrisBomb; set => hasDebrisBomb = value; }
    public bool HasSpeedUp { get => hasSpeedUp; set => hasSpeedUp = value; }


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        player = GetComponent<Player>();
    }

    public void UsePower()
    {
        if (hasPowerUp)
        {
            if (hasDebrisBomb)
            {
                Debug.LogError("threw debris bomb");
                hasDebrisBomb = false;
                ui.UpdatePowerUI();
            }
            else if (HasSpeedUp)
            {
                Debug.LogError("used speedup");
                player.SprintBuffed = true;
                HasSpeedUp = false;
                ui.UpdatePowerUI(2);
                StartCoroutine(CancelPowers(2, 5f));
            }
            hasPowerUp = false;
        }

        else
        {
            Debug.LogError("No current power");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Power" && !hasPowerUp)
        {
            Destroy(col.gameObject);
            RollRandomPower();
            hasPowerUp = true;
            Debug.Log("collide with power");
        }
    }

    void RollRandomPower()
    {

        int num = 2;
        //num = Random.Range(1, 2);
        if (num == 1)
        {
            HasDebrisBomb = true;
        }
        else if (num == 2)
        {
            HasSpeedUp = true;
        }
        else
        {
            HasDebrisBomb = true;
        }

        ui.UpdatePowerUI();
    }


    IEnumerator CancelPowers(int powerNum, float sec)
    {
        yield return new WaitForSeconds(sec);
        if(powerNum == 2)
        {
            player.SprintBuffed = false;
            ui.UpdatePowerUI(2);
            Debug.LogError("Coroutine end");
        }
    }

}


