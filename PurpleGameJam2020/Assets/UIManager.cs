using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Player p1;
    [SerializeField] Player p2;

    //Energy
    [SerializeField] Slider p1EnergySlider;
    [SerializeField] Image p1SliderFill;
    [SerializeField] Image p1SliderBg;
    [SerializeField] Slider p2EnergySlider;
    [SerializeField] Image p2SliderFill;
    [SerializeField] Image p2SliderBg;

    //Powerups
    [SerializeField] Image p1Power;
    [SerializeField] Image p2Power;


    Color defaultColor;
    Color defaultFillColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = p1SliderBg.color;
        defaultFillColor = p1SliderFill.color;
    }

    // Update is called once per frame
    void Update()
    {
        p1EnergySlider.value = p1.SprintMeter;
        p2EnergySlider.value = p2.SprintMeter;

        CheckEnergyLocks();
    }

    void CheckEnergyLocks()
    {
        if (p1.SprintLocked)
        {
            p1SliderBg.color = Color.red;
        }
        else if (!p1.SprintLocked)
        {
            p1SliderBg.color = defaultColor;
        }
        if (p2.SprintLocked)
        {
            p2SliderBg.color = Color.red;
        }
        else if (!p2.SprintLocked)
        {
            p2SliderBg.color = defaultColor;
        }

        
    }


    /// <summary>
    /// Num : The number of the powerup 
    ///     1 - DebrisBomb
    ///     2 - Speedpowerup
    /// Bool : Are you Enabling or Disabling the powerup
    /// </summary>
    /// <param name="num"></param>
    /// <param name="enable"></param>
    public void UpdatePowerUI(int num = 0, bool enable = true)
    {
        if (enable)
        {
            if (p1.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;
                p1Power.enabled = true;
            }
            else if (p2.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;
                p2Power.enabled = true;
            }

            if (p1.GetComponent<PowerupManager>().HasSpeedUp)
            {
                //p1Power.sprite = speedupSprite;
                p1Power.enabled = true;
            }
            else if (p2.GetComponent<PowerupManager>().HasSpeedUp)
            {
                //p1Power.sprite = speedupSprite;
                p2Power.enabled = true;
            }
        }
        

        else if (!enable)
        {
            //Disable speed buff
            if (num == 2)
            {
                if (p1.SprintBuffed)
                {
                    p1SliderFill.color = Color.white;
                }
                else if (p2.SprintBuffed)
                {
                    p2SliderFill.color = Color.white;
                }
                if (!p1.SprintBuffed)
                {
                    p1SliderFill.color = defaultFillColor;
                }
                else if (!p2.SprintBuffed)
                {
                    p2SliderFill.color = defaultFillColor;
                }
            }


        }


        //Speedup Power Check
        
    }
}
