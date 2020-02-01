using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    [SerializeField] GameObject debrisBomb;
    [SerializeField] SphereCollider sphere;

    Player player;
    UIManager ui;
    SphereCollider debrisSphere;

    float powerTimer = 5f;

    bool hasPowerUp;
    bool hasDebrisBomb;
    bool hasSpeedUp;
    bool hasMagnet;

    public bool HasDebrisBomb { get => hasDebrisBomb; set => hasDebrisBomb = value; }
    public bool HasSpeedUp { get => hasSpeedUp; set => hasSpeedUp = value; }
    public bool HasMagnet { get => hasMagnet; set => hasMagnet = value; }


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        player = GetComponent<Player>();
        //debrisSphere = GetComponent<SphereCollider>();
    }

    public void UsePower()
    {
        if (hasPowerUp)
        {
            if (hasDebrisBomb) //1
            {
                GameObject bullet = Instantiate(debrisBomb, transform.forward, Quaternion.identity) as GameObject; //use arm forward
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                Debug.LogError("threw debris bomb");
                hasDebrisBomb = false;
                ui.UpdatePowerUI();
            }
            else if (HasSpeedUp) //2
            {
                Debug.LogError("used speedup");
                player.SprintBuffed = true;
                HasSpeedUp = false;
                ui.UpdatePowerUI(2, false);
                StartCoroutine(CancelPowers(2, 5f));
            }
            else if (hasMagnet) //3
            {
                Debug.LogError("used magnet power");
                sphere.radius = 6f;
                StartCoroutine(CancelPowers(3, 5f));
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

        int num = 3;
        //num = Random.Range(1, 2);
        if (num == 1)
        {
            HasDebrisBomb = true;
        }
        else if (num == 2)
        {
            HasSpeedUp = true;
        }
        else if(num == 3) 
        {
            HasMagnet = true;
        }

        ui.UpdatePowerUI();
    }


    IEnumerator CancelPowers(int powerNum, float sec)
    {
        yield return new WaitForSeconds(sec);
        if(powerNum == 2)
        {
            player.SprintBuffed = false;
            ui.UpdatePowerUI(2, false);
            Debug.LogError("Coroutine end");
        }
        if (powerNum == 3)
        {
            hasMagnet = false;
            // ui.UpdatePowerUI(3, false);
            sphere.radius = 2f;
            Debug.LogError("Coroutine end");
        }
    }

}


