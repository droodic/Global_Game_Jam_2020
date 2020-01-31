using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private CharacterController _characterController;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private bool _player2 = false;
    [SerializeField] private GameObject _arm;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MovePlayer();
        MoveArm();

    }

    private void MovePlayer()
    {
        var newSpeed = _speed;
        string sprintButton = _player2 ? "Sprint2" : "Sprint";
        if (Input.GetButton(sprintButton))
        {
            newSpeed = _speed * 3.0f;
        }
        string xAxis = _player2 ? "Horizontal2" : "Horizontal";
        string yAxis = _player2 ? "Vertical2" : "Vertical";
        var x = Input.GetAxis(xAxis) * Time.deltaTime * newSpeed;
        var y = Input.GetAxis(yAxis) * Time.deltaTime * newSpeed;

        var move = _camera.transform.right * x + _camera.transform.forward * y;
        move.y = 0.0f;
        _characterController.Move(move);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void MoveArm()
    {

        var newSpeed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            newSpeed = _speed * 3.0f;
        }
        string xAxis = _player2 ? "HorizontalArm2" : "HorizontalArm";
        string yAxis = _player2 ? "VerticalArm2" : "VerticalArm";
        var x = Input.GetAxis(xAxis);
        var y = Input.GetAxis(yAxis);
        if (x == 0 && y == 0)
        {
            return;
        }
        var move = _camera.transform.right * x + _camera.transform.forward * y;
        move.y = 0.0f;
        var rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
        _arm.transform.rotation = rotation;
    }
}
