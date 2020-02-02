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

    GameManager game;

    Player p1;
    Player p2;

    InventoryManager P1im;
    InventoryManager P2im;

    [SerializeField] Text timerText;

    //VP
    [SerializeField] Text p1Vp;
    [SerializeField] Text p2Vp;
    [SerializeField] Text p1Bonus;
    [SerializeField] Text p2Bonus;

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
    [SerializeField] Sprite speedPower;
    [SerializeField] Sprite magnetPower;
    [SerializeField] Sprite forcefieldPower;

    [SerializeField] Image p1Power;
    [SerializeField] Image p2Power;
    [SerializeField] Animation p1PowerAnim;
    [SerializeField] Animation p2PowerAnim;

    //VictoryPanel
    [SerializeField] GameObject victoryPanel;

    Color defaultColor;
    Color defaultFillColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = p1SliderBg.color;
        defaultFillColor = p1SliderFill.color;
        game = FindObjectOfType<GameManager>();
        victoryPanel.gameObject.SetActive(false);
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
        if (game.Playing)
        {
            timerText.text = game.RoundTime.ToString("F0");

        }

    }

    public void UpdateVp()
    {
        p1Vp.text = "Points: " + p1.VictoryPoints.ToString();
        p2Vp.text = "Points: " + p2.VictoryPoints.ToString();
    }

    public void UpdateWithBonus(Player player, int num)
    {
        if(player.tag == "Player")
        {
            p1Bonus.text = "Complete! +" + num;
            p1Bonus.enabled = true;
            p1Bonus.GetComponent<Animation>().Play();
        }
        else if(player.tag == "Player2")
        {
            p2Bonus.text = "Complete! +" + num;
            p2Bonus.enabled = true;
            p2Bonus.GetComponent<Animation>().Play();
        }

        Invoke("HideBonus", 3.5f);
    }

    void HideBonus()
    {
        p1Bonus.enabled = false;
        p2Bonus.enabled = false;
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


    public void DisablePowerBtn(Player player)
    {
        if(player.tag == "Player")
        { 
            p1Power.enabled = false;
        }
        else
        {
            p2Power.enabled = false;
        }
        
    }

    /// <summary>
    /// Num : The number of the powerup 
    ///     1 - DebrisBomb
    ///     2 - Speedpowerup
    ///     3 - Magnet
    ///     4 - Forcefield
    /// Bool : Are you Enabling or Disabling the powerup
    /// Change sprite and required UI element for specific power (energy bar becomes white for speedup, etc)
    /// </summary>
    /// <param name="num"></param>
    /// <param name="enable"></param>
    /// 

    public void UpdatePowerUI(Player player, int num = 0, bool enable = true)
    {
        if (player.tag == "Player" && enable)
        {

            if (player.GetComponent<PowerupManager>().HasSpeedUp)
            {
                p1Power.sprite = speedPower;

            }
            else if (player.GetComponent<PowerupManager>().HasMagnet)
            {
                p1Power.sprite = magnetPower;
            }

            else if (player.GetComponent<PowerupManager>().HasForceField)
            {
                p1Power.sprite = forcefieldPower;
            }
            p1Power.enabled = true;
            p1PowerAnim.Play();
        }
        else if (player.tag == "Player2" && enable)
        {
            if (player.GetComponent<PowerupManager>().HasSpeedUp)
            {
                p2Power.sprite = speedPower;

            }
            else if (player.GetComponent<PowerupManager>().HasMagnet)
            {
                p2Power.sprite = magnetPower;

            }

            else if(player.GetComponent<PowerupManager>().HasForceField)
            {
                p2Power.sprite = forcefieldPower;
            }
            p2Power.enabled = true;
            p2PowerAnim.Play();
        }

        if (player.tag == "Player" && !enable)
        {
            //Disable speed buff
            if (num == 2)
            {
                if (P1.SprintBuffed)
                {
                    p1SliderFill.color = Color.white;
                }
                else if (!P1.SprintBuffed)
                {
                    p1SliderFill.color = defaultFillColor;
                }

            }

            
            
        }
        else if (player.tag == "Player2" && !enable)
        {
            if (num == 2)
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

    }

    //Speedup Power Check

    //activate and deactivate victorypanel
    public void EndRoundUI()
    {
        victoryPanel.gameObject.SetActive(true);
    }
}
