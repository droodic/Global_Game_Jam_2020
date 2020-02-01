using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //#region Singleton
    //private static InputManager _instance = null;
    //public static InputManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindObjectOfType<InputManager>();
    //        }
    //        return _instance;
    //    }
    //}

    //#endregion

    private Vector2 _moveAxis;
    public Vector2 MoveAxis { get => _moveAxis; set => _moveAxis = value; }

    private Vector2 _aimAxis;
    public Vector2 AimAxis { get => _aimAxis; set => _aimAxis = value; }

    private bool _sprinting;
    public bool Sprinting { get => _sprinting; set => _sprinting = value; }

    private PlayerInput _playerInput;
    private bool mouseAndKey;
    public bool MouseAndKey { get => mouseAndKey; }

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerInput = GetComponent<PlayerInput>();
        mouseAndKey = _playerInput.devices[0].device.Equals(Mouse.current) || _playerInput.devices[0].device.Equals(Keyboard.current);
        Debug.Log(MouseAndKey);
        _moveAxis = Vector2.zero;
        _aimAxis = Vector2.zero;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimAxis = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            _sprinting = true;
        }
        else if (context.ReadValue<float>() == 0)
        {
            _sprinting = false;
        }
    }

    public void OnPower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_player != null)
            {

            _player.Power.UsePower();
            Debug.Log(context);
            }
        }
        
    }
}
