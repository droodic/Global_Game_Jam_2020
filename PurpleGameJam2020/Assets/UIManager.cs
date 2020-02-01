using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Player p1;
    [SerializeField] Player p2;

    [SerializeField] Slider p1EnergySlider;
    [SerializeField] Image p1SliderBg;
    [SerializeField] Slider p2EnergySlider;
    [SerializeField] Image p2SliderBg;

    Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = p1SliderBg.color;
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
}
