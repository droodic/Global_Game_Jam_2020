using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sprintMeter;
    InputManager _inputManager;
    bool sprinting;

    bool sprintLocked;
    bool sprintBuffed;

    float lockTimer = 5f;

    public float SprintMeter { get => sprintMeter; set => sprintMeter = value; }
    public bool SprintLocked { get => sprintLocked; set => sprintLocked = value; }
    public bool SprintBuffed { get => sprintBuffed; set => sprintBuffed = value; }

    //bool player2;
    public Gamepad GP { get => _movement.Gamepad; }
    public InputManager InputManager { get => _inputManager; }
    public PowerupManager Power { get => power; }

    private Movement _movement;
    PowerupManager power;
    // Start is called before the first frame update
    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _movement = GetComponent<Movement>();
        power = GetComponent<PowerupManager>();
        CameraRig.Instance.AddPlayer(this);
        //player2 = this.gameObject.tag == "Player2";

    }

    // Input tracker
    void Update()
    {
        if (_inputManager.Sprinting && !SprintLocked)
        {
            Sprint();
        }

        if (!_inputManager.Sprinting || !sprinting)
        {
            ClearSprint(SprintLocked);
        }
        //Call power
        //if (GP.buttonWest.wasPressedThisFrame)
        //{
        //    power.UsePower();
        //}
        //
        //if (GP != null)
        //{
        //    if (GP.rightShoulder.ReadValue() > 0 && !SprintLocked)
        //    {
        //        Sprint();
        //    }
        //
        //    if (GP.rightShoulder.ReadValue() == 0 || !sprinting)
        //    {
        //        ClearSprint(SprintLocked);
        //    }
        //    //Call power
        //    if (GP.buttonWest.wasPressedThisFrame)
        //    {
        //        power.UsePower();
        //    }
        //}
        //else
        //{
        //    if (PlayerManager.Instance.Players[0] == this)
        //    {
        //        if (Keyboard.current.shiftKey.ReadValue() > 0 && !SprintLocked)
        //        {
        //            Sprint();
        //        }
        //
        //        if (Keyboard.current.shiftKey.ReadValue() == 0 || !sprinting)
        //        {
        //            ClearSprint(SprintLocked);
        //        }
        //    }
        //}

        //Debug Switch
        //if (Keyboard.current.pKey.wasPressedThisFrame)
        //{
        //    //player2 = !player2;
        //    //Debug.LogError(player2 + this.gameObject.name);
        //}


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

            //Debug.Log("Sprintmeter" + SprintMeter.ToString());
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
                //Debug.Log("Sprintmeter" + SprintMeter.ToString());
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
                //Debug.Log("Sprintmeter" + SprintMeter.ToString());
            }
            if (SprintMeter >= 50)
            {
                sprinting = true;
                SprintLocked = false;
                SprintMeter = 50;
            }
        }
    }

}
