using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Player player;
    private Camera _camera;
    private CharacterController _characterController;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private bool _player2 = false;
    [SerializeField] private GameObject _arm;
    [SerializeField] private GameObject _body;
    private Gamepad _gamepad;
    private Keyboard _keyboard;
    public Gamepad Gamepad { get => _gamepad; set => _gamepad = value; }
    public GameObject Arm { get => _arm; set => _arm = value; }

    InputAction _WASD;
    InputAction _MOUSE;
    private Vector2 _mousePos;

    private void Start()
    {

        player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        SetupKeyboardAndMouseInput();
        SetupGamepad();

    }

    private void SetupKeyboardAndMouseInput()
    {
        _WASD = new InputAction(name: "move");
        _WASD.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _MOUSE = new InputAction(name: "mouse");
        _MOUSE.AddBinding("Vector2").WithPath("<Mouse>/position");
        _keyboard = Keyboard.current;
    }

    private void SetupGamepad()
    {
        if (Gamepad.all.Count > 1)
        {
            _gamepad = Gamepad.all[_player2 ? 1 : 0];
        }
        else
        {
            _gamepad = Gamepad.current;
            _MOUSE.Enable();
            _WASD.Enable();
            if (_player2)
            {
                _gamepad = null;
                _WASD.Disable();
                _MOUSE.Disable();
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            _player2 = !_player2;
            SetupGamepad();
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
        MoveArm();
    }

    private void MovePlayer()
    {
        var newSpeed = _speed;
        if (player.InputManager.Sprinting && !player.SprintLocked)
        {
            newSpeed = _speed * 3.0f;
        }
        Vector2 vect = player.InputManager.MoveAxis;
        Vector3 move;
        if (vect == Vector2.zero)
        {
            return;
        }
        move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
        move.y = 0.0f;
        _characterController.Move(move * Time.deltaTime * newSpeed);
        var rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
        _body.transform.rotation = rotation;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void MoveArm()
    {
        Vector2 vect;
        Vector3 move;
        Quaternion rotation;
        if (player.InputManager.MouseAndKey)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(player.InputManager.AimAxis);
            Physics.Raycast(ray, out raycastHit, 1000.0f, LayerMask.GetMask("Floor"));
             vect = new Vector2(raycastHit.point.z, raycastHit.point.x);
            move = raycastHit.point;
            move.y = Arm.transform.position.y;
            Arm.transform.LookAt(move);
        }
        else
        {
            vect = player.InputManager.AimAxis;
            move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
            move.y = 0.0f;
            if (move == Vector3.zero)
            {
                return;
            }
            rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
            Arm.transform.rotation = rotation;
        }


    }
}
