using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sprintMeter;

    bool sprinting;
    public float SprintMeter { get => sprintMeter; set => sprintMeter = value; }

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

        if (Input.GetButton(ButtonName))
        {
            Sprint();
        }

        if(Input.GetButtonUp(ButtonName) || !sprinting)
        {
            ClearSprint();
        }

        //Debug Switch
        if (Input.GetKeyDown(KeyCode.P))
        {
            player2 = !player2;
        }
    }

    void Sprint()
    {
        if (!sprinting) sprinting = true; //onlyonce

        if (sprinting && SprintMeter > 0)
        {
            SprintMeter--;
           // p1slider.value = SprintMeter;
        }


        Debug.Log("Sprintmeter" + SprintMeter.ToString());

    }
    void ClearSprint()
    {
        if (sprinting) sprinting = false;
        if(SprintMeter < 50)
        {
            SprintMeter += 0.25f;
            //p1slider.value = SprintMeter;
            Debug.Log("Sprintmeter" + SprintMeter.ToString());
        }
        else if (SprintMeter.Equals(50))
        {
            sprinting = true;
        }
    }
}
