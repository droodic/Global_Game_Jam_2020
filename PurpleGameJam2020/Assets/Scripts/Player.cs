using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Color Choices")]
    [SerializeField] private List<Material> _colors;
    [SerializeField] private MeshRenderer _bodyMR;
    [SerializeField] private MeshRenderer _armMR;
    [SerializeField] float sprintMeter;
    InputManager _inputManager;

    #region Sounds
    public AudioClip aspirationSound;
    public AudioClip motorSound;
    #endregion

    int victoryPoints;
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
    public List<Material> Colors { get => _colors; }
    public int VictoryPoints { get => victoryPoints; set => victoryPoints = value; }

    private Movement _movement;
    PowerupManager power;
    // Start is called before the first frame update
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
    }

    void Start()
    {
        //_inputManager = GetComponent<InputManager>();
        _movement = GetComponent<Movement>();
        power = GetComponent<PowerupManager>();
        CameraRig.Instance.AddPlayer(this);
        PlayerManager.Instance.AddPlayer(this);
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
    }

    void Sprint()
    {
        if (!SprintBuffed)
        {
            if (!sprinting) sprinting = true; //onlyonce

            if (sprinting && SprintMeter > 0)
            {
                SprintMeter -= 0.25f;
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
                SprintMeter += 0.18f;
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
                SprintMeter += 0.145f;
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

    public void ChangeColor(Material color)
    {
        if (color != null)
        {
            var mats = _bodyMR.materials;
            mats[2] = color;
            _bodyMR.materials = mats;
            _armMR.material = color;
        }
    }
}
