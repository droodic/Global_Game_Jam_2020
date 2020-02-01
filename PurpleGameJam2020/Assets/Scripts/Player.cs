using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sprintMeter;

    bool sprinting;

    bool sprintLocked;
    float lockTimer = 5f;

    public float SprintMeter { get => sprintMeter; set => sprintMeter = value; }
    public bool SprintLocked { get => sprintLocked; set => sprintLocked = value; }

    bool player2;

    // Start is called before the first frame update
    void Start()
    {
        player2 = this.gameObject.tag == "Player2";
    }

    // Update is called once per frame
    void Update()
    {
        string ButtonName = player2 ? "Sprint2" : "Sprint";

        if (Input.GetButton(ButtonName) && !SprintLocked)
        {
            Sprint();
        }

        if(Input.GetButtonUp(ButtonName) || !sprinting)
        {
            ClearSprint(SprintLocked);
        }

        //Debug Switch
        if (Input.GetKeyDown(KeyCode.P))
        {
            player2 = !player2;
        }

        if (SprintLocked && lockTimer > 0)
        {
            lockTimer -= Time.deltaTime;
        }
        else if(SprintLocked && lockTimer <= 0)
        {
            SprintLocked = false;
            lockTimer = 5f;
        }

    }

    void Sprint()
    {
        if (!sprinting) sprinting = true; //onlyonce

        if (sprinting && SprintMeter > 0)
        {
            SprintMeter-=0.75f;
           // p1slider.value = SprintMeter;
        }
        else if (SprintMeter <= 0)
        {
            SprintMeter = 0;
            SprintLocked = true;
        }

        Debug.Log("Sprintmeter" + SprintMeter.ToString());
    }

    void ClearSprint(bool locked)
    {
        if (!locked)
        {
            if (sprinting) sprinting = false;
            if (SprintMeter < 50)
            {
                SprintMeter += 0.25f;
                //p1slider.value = SprintMeter;
                Debug.Log("Sprintmeter" + SprintMeter.ToString());
            }
            else if (SprintMeter.Equals(50))
            {
                sprinting = true;
                SprintLocked = false;
            }
        }
        else if (locked)
        {
            if (sprinting) sprinting = false;
            if (SprintMeter < 50)
            {
                SprintMeter += 0.175f;
                //p1slider.value = SprintMeter;
                Debug.Log("Sprintmeter" + SprintMeter.ToString());
            }
            else if (SprintMeter.Equals(50))
            {
                sprinting = true;
                SprintLocked = false;
            }
        }
    }

}
