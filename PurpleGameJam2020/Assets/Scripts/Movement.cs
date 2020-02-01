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

    public Gamepad Gamepad { get => _gamepad; set => _gamepad = value; }

    private void Start()
    {
        player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        SetupGamepad();

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
            if (_player2)
            {
                _gamepad = null;
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.pKey.ReadValue() > 0)
        {
            _player2 = !_player2;
            SetupGamepad();

        }
    }
    private void LateUpdate()
    {
        if (_gamepad != null)
        {
            MovePlayer();
            MoveArm(); 
        }

    }

    private void MovePlayer()
    {
        var newSpeed = _speed;
        //string sprintButton = _player2 ? "Sprint2" : "Sprint";
        //if (Input.GetButton(sprintButton))
        if (_gamepad.rightShoulder.ReadValue() > 0 && !player.SprintLocked)
        {
            newSpeed = _speed * 3.0f;
        }
        //string xAxis = _player2 ? "Horizontal2" : "Horizontal";
        //string yAxis = _player2 ? "Vertical2" : "Vertical";
        //var x = Input.GetAxis(xAxis) * Time.deltaTime * newSpeed;
        //var y = Input.GetAxis(yAxis) * Time.deltaTime * newSpeed;
        var vect = _gamepad.leftStick.ReadValue() * Time.deltaTime * newSpeed;
        var move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
        move.y = 0.0f;
        _characterController.Move(move);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        var rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
        _body.transform.rotation = rotation;
    }

    private void MoveArm()
    { 
       
        //string xAxis = _player2 ? "HorizontalArm2" : "HorizontalArm";
        //string yAxis = _player2 ? "VerticalArm2" : "VerticalArm";
        //var x = Input.GetAxis(xAxis);
        //var y = Input.GetAxis(yAxis);
        var vect = _gamepad.rightStick.ReadValue();
        if (vect.x == 0 && vect.y == 0)
        {
            return;
        }
        var move = _camera.transform.right * vect.x + _camera.transform.forward * vect.y;
        move.y = 0.0f;
        var rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
        _arm.transform.rotation = rotation;
    }
}
