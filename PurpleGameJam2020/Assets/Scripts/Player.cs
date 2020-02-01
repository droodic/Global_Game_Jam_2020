using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] Slider p1slider;
    [SerializeField] float sprintMeter;

    bool sprinting;

    public float SprintMeter { get => sprintMeter; set => sprintMeter = value; }

    // Start is called before the first frame update
    void Start()
    {
        p1slider.maxValue = SprintMeter;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Sprint"))
        {
            Sprint();
        }

        if(Input.GetButtonUp("Sprint") || !sprinting)
        {
            ClearSprint();
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
          //  p1slider.value = SprintMeter;
            Debug.Log("Sprintmeter" + SprintMeter.ToString());
        }
        else if (SprintMeter.Equals(p1slider.maxValue))
        {
            sprinting = true;
        }
    }
}
