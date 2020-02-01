using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    UIManager ui;
    bool hasDebrisBomb;

    public bool HasDebrisBomb { get => hasDebrisBomb; set => hasDebrisBomb = value; }


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
    }

    public void UsePower()
    {
        if (hasDebrisBomb)
        {
            Debug.LogError("threw debris bomb");
            hasDebrisBomb = false;
            ui.UpdatePowerUI();
        }
        else
        {
            Debug.LogError("No current power");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Power")
        {
            Destroy(col.gameObject);
            RollRandomPower();
            Debug.Log("collide with power");
        }
    }

    void RollRandomPower()
    {
        int num = 1;
        //num = Random.Range(1, 3);
        if(num == 1)
        {
            HasDebrisBomb = true;
        }
        /*else if(num == 2)
        {
            HasDebrisBomb = true;
        }
        else
        {
            HasDebrisBomb = true;
        }
        */
        ui.UpdatePowerUI();
    }
}


