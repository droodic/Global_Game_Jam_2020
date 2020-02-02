using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector2 _moveAxis;
    public Vector2 MoveAxis { get => _moveAxis; }

    private Vector2 _aimAxis;
    public Vector2 AimAxis { get => _aimAxis; }

    private bool _sprinting;
    public bool Sprinting { get => _sprinting; }

    private bool _repairing;
    public bool Repairing { get => _repairing; }

    private PlayerInput _playerInput;
    private bool mouseAndKey;
    public bool MouseAndKey { get => mouseAndKey; }

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput.devices.Count > 0)
        {
            mouseAndKey = _playerInput.devices[0].device.Equals(Mouse.current) || _playerInput.devices[0].device.Equals(Keyboard.current);
        }
        Debug.Log(MouseAndKey);
        _moveAxis = Vector2.zero;
        _aimAxis = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
        SoundManager.Instance.PlayVectorSound(_moveAxis);
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
            _player.Power.UsePower();
        }
    }

    public void OnRepair(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            _repairing = true;
            SoundManager.Instance.Repair(context.ReadValue<float>());
        }
        else if (context.ReadValue<float>() == 0)
        {
            _repairing = false;
            SoundManager.Instance.Repair(context.ReadValue<float>());
        }
    }
}
