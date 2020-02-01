using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public Player P1 { get => p1; set => p1 = value; }
    public Player P2 { get => p2; set => p2 = value; }
    #endregion

    Player p1;
    Player p2;

    InventoryManager P1im;
    InventoryManager P2im;

    //Debris
    [SerializeField] Slider p1DebrisSlider;
    [SerializeField] Slider p2DebrisSlider;

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
    [SerializeField] Animation p1PowerAnim;
    [SerializeField] Animation p2PowerAnim;

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
        if ((P1 != null && P2 != null))
        {
            p1EnergySlider.value = P1.SprintMeter;
            p2EnergySlider.value = P2.SprintMeter;
            CheckEnergyLocks();
        }

    }

    public void UpdateDebrisUI()
    {
        p1DebrisSlider.value = P1.GetComponent<InventoryManager>().DebrisCount;
        p2DebrisSlider.value = P2.GetComponent<InventoryManager>().DebrisCount;

    }

    void CheckEnergyLocks()
    {
        if (P1.SprintLocked)
        {
            p1SliderBg.color = Color.red;
        }
        else if (!P1.SprintLocked)
        {
            p1SliderBg.color = defaultColor;
        }
        if (P2.SprintLocked)
        {
            p2SliderBg.color = Color.red;
        }
        else if (!P2.SprintLocked)
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
    public void UpdatePowerUI(Player player, int num = 0, bool enable = true)
    {
        if (player.tag == "Player" && enable)
        {

            if (player.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;

            }
            else if (player.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;

            }

            if (player.GetComponent<PowerupManager>().HasSpeedUp)
            {
                //p1Power.sprite = speedupSprite;
            }
            p1Power.enabled = true;
            p1PowerAnim.Play();
        }
        else if (player.tag == "Player2" && enable)
        {

            if (player.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;

            }
            else if (player.GetComponent<PowerupManager>().HasDebrisBomb)
            {
                //p1Power.sprite = bombsprite;

            }

            if (player.GetComponent<PowerupManager>().HasSpeedUp)
            {
                //p1Power.sprite = speedupSprite;

            }
            p2Power.enabled = true;
            p2PowerAnim.Play();
        }

        if(player.tag == "Player" && !enable)
        {
            //Disable speed buff
            if (num == 2)
            {
                if (P1.SprintBuffed)
                {
                    p1SliderFill.color = Color.white;
               
                if (!P1.SprintBuffed)
                {
                    p1SliderFill.color = defaultFillColor;
                }

            }

            p1Power.enabled = false;
            p2Power.enabled = false;
            }

        }
        else if(player.tag == "Player1" && !enable)
        {
            if (P2.SprintBuffed)
            {
                p2SliderFill.color = Color.white;
            }
            else if (!P2.SprintBuffed)
            {
                p2SliderFill.color = defaultFillColor;
            }
        }

    }

    //Speedup Power Check

}
