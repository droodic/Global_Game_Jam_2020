using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Player p1;
    [SerializeField] Player p2;

    [SerializeField] Slider p1EnergySlider;
    [SerializeField] Slider p2EnergySlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        p1EnergySlider.value = p1.SprintMeter;
        p2EnergySlider.value = p2.SprintMeter;
    }
}
