using System.Collections;
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
                Debug.Log("threw debris bomb");
                hasDebrisBomb = false;
                ui.UpdatePowerUI(player, 1, false);
            }
            else if (HasSpeedUp) //2
            {
                Debug.Log("used speedup");
                player.SprintBuffed = true;
                HasSpeedUp = false;
                ui.UpdatePowerUI(player, 2, false);
                StartCoroutine(CancelPowers(2, 5f));
            }
            else if (hasMagnet) //3
            {
                Debug.Log("used magnet power");
                ui.UpdatePowerUI(player, 3, false);
                sphere.radius = 14f;
                StartCoroutine(CancelPowers(3, 5f));
            }
            else if (hasForceField) //4
            {
                Debug.Log("spawned force field");
                ui.UpdatePowerUI(player, 4, false);
                if (player == PlayerManager.Instance.Players[0])
                {
                    Destroy(Instantiate(forceFieldPlayer1, this.transform.position, forceFieldPlayer1.transform.rotation, null).gameObject, 5f);
                }
                else if (player == PlayerManager.Instance.Players[1])
                {
                    Destroy(Instantiate(forceFieldPlayer2, this.transform.position, forceFieldPlayer2.transform.rotation, null).gameObject, 5f);
                }
                StartCoroutine(CancelPowers(4, 10f));
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


        int num = Random.Range(3, 5);
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
        else if (num == 4)
        {
            HasForceField = true;
        }
        Debug.Log(num);
        HasPowerUp = true;
        ui.UpdatePowerUI(player);
    }


    IEnumerator CancelPowers(int powerNum, float sec)
    {
        yield return new WaitForSeconds(sec);
        if (powerNum == 2 && HasSpeedUp)
        {
            HasSpeedUp = false;
            player.SprintBuffed = false;
            ui.UpdatePowerUI(player, 2, false);
            Debug.LogError("Coroutine end");
        }
        if (powerNum == 3 && hasMagnet)
        {
            hasMagnet = false;
            sphere.radius = 4f;
            Debug.LogError("Coroutine end");
        }
        if (powerNum == 4 && HasForceField)
        {
            hasForceField = false;
            Debug.LogError("Coroutine end");
        }

    }
}


