using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sprintMeter;
    
    bool sprinting;

    bool sprintLocked;
    bool sprintBuffed;

    float lockTimer = 5f;

    public float SprintMeter { get => sprintMeter; set => sprintMeter = value; }
    public bool SprintLocked { get => sprintLocked; set => sprintLocked = value; }
    public bool SprintBuffed { get => sprintBuffed; set => sprintBuffed = value; }

    bool player2;
    Gamepad gp;
    PowerupManager power;
    // Start is called before the first frame update
    void Start()
    {
        gp = GetComponent<Movement>().Gamepad;
        power = GetComponent<PowerupManager>();
        player2 = this.gameObject.tag == "Player2";
 
    }

    // Input tracker
    void Update()
    {
        //string ButtonName = player2 ? "Sprint2" : "Sprint";

        if (gp != null) 
        {
            if (gp.rightShoulder.ReadValue() > 0 && !SprintLocked)
            {
                Sprint();
            }

            if (gp.rightShoulder.ReadValue() == 0 || !sprinting)
            {
                ClearSprint(SprintLocked);
            }
            //Call power
            if (gp.buttonWest.wasPressedThisFrame)
            {
                power.UsePower();
            }
        }

        //Debug Switch
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            player2 = !player2;
            gp = GetComponent<Movement>().Gamepad;
            Debug.LogError(player2 + this.gameObject.name);
        }

        
    }

    void Sprint()
    {
        if (!SprintBuffed)
        {
            if (!sprinting) sprinting = true; //onlyonce

            if (sprinting && SprintMeter > 0)
            {
                SprintMeter -= 0.55f;
                // p1slider.value = SprintMeter;
            }
            else if (SprintMeter <= 0)
            {
                SprintMeter = 0;
                SprintLocked = true;
            }

            Debug.Log("Sprintmeter" + SprintMeter.ToString());
        }
        
        else if (SprintBuffed)
        {
            SprintMeter = 50f;
        }
    }

    void ClearSprint(bool locked)
    {
        if (!locked)
        {
            if (sprinting) sprinting = false;
            if (SprintMeter < 50)
            {
                SprintMeter += 0.25f;
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
            if (SprintMeter>= 50)
            {
                sprinting = true;
                SprintLocked = false;
                SprintMeter = 50;
            }
        }
    }

}
