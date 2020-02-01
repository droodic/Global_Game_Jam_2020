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
        _WASD = new InputAction(name: "move");
        _WASD.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _MOUSE = new InputAction(name: "mouse");
        _MOUSE.AddBinding("Vector2").WithPath("<Mouse>/position");
        player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        SetupGamepad();
        _keyboard = Keyboard.current;

    }
    private void OnMove(InputAction.CallbackContext context)
    {
        Debug.LogError("AIMING" + context.ToString());
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

        _mousePos = _MOUSE.ReadValue<Vector2>();
    }
    private void LateUpdate()
    {
        MovePlayer();
        MoveArm();
    }

    private void MovePlayer()
    {
        var newSpeed = _speed;
        
        Vector2 vect = _WASD.ReadValue<Vector2>() * Time.deltaTime * newSpeed;
        if (_gamepad != null)
        {
            vect = _gamepad.leftStick.ReadValue() * Time.deltaTime * newSpeed;
            if (_gamepad.rightShoulder.ReadValue() > 0 && !player.SprintLocked)
            {
                newSpeed = _speed * 3.0f;
            }
        }
        if (vect == Vector2.zero)
        {
            return;
        }
        var move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
        move.y = 0.0f;
        _characterController.Move(move);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        var rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
        _body.transform.rotation = rotation;
    }

    private void MoveArm()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, 1000.0f);
        Debug.DrawRay(ray.origin, ray.direction *raycastHit.distance,Color.red,3.0f);
        Vector2 vect = new Vector2(raycastHit.point.z, raycastHit.point.x);
        Debug.Log(raycastHit.point);
        Vector3 move;
        Quaternion rotation;
        if (_gamepad != null)
        {
            vect = _gamepad.rightStick.ReadValue();
            move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
            move.y = 0.0f;
            rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
            Arm.transform.rotation = rotation;
        }
        else
        {
            move = raycastHit.point;
            move.y = Arm.transform.position.y;
            Arm.transform.LookAt(move);
        }
        if (vect.x == 0 && vect.y == 0)
        {
            return;
        }

    
    }
}
