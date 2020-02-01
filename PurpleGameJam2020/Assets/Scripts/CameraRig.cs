using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{

    #region Singleton
    private static CameraRig _instance = null;
    public static CameraRig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraRig>();
            }
            return _instance;
        }
    }

    //public Transform[] Players { get => _players; set => _players = value; }

    #endregion

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float _screenRim = 4f;
    [SerializeField] private float _minimumSize = 6.5f;
    private List<Transform> _players;
    private float _zoomSpeed;
    private Vector3 _smoothVelocity;
    private Vector3 _camRigPosition;
    private Camera _camera;

    // Start is called before the first frame update
    private void Awake()
    {
        _players = new List<Transform>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        GetAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, _camRigPosition, ref _smoothVelocity, smoothTime);
    }


    private void GetAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int qtyOfPlayers = 0;
        foreach (var player in _players)
        {
            if (player.gameObject.activeSelf)
            {
                averagePosition += player.position;
                qtyOfPlayers++;
            }
        }

        if (qtyOfPlayers != 0)
            averagePosition /= qtyOfPlayers;

        averagePosition.y = transform.position.y;

        _camRigPosition = averagePosition;
    }


    private void Zoom()
    {
        float cameraSize = GetCameraSize();
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, cameraSize, ref _zoomSpeed, smoothTime);
    }

    private float GetCameraSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(_camRigPosition);

        float screenSize = 0f;

        foreach (var player in _players)
        {
            if (player.gameObject.activeSelf)
            {
                Vector3 targetLocalPos = transform.InverseTransformPoint(player.position);

                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                screenSize = Mathf.Max(screenSize, Mathf.Abs(desiredPosToTarget.y));

                screenSize = Mathf.Max(screenSize, Mathf.Abs(desiredPosToTarget.x) / _camera.aspect);
            }
        }

        screenSize += _screenRim;

        screenSize = Mathf.Max(screenSize, _minimumSize);

        return screenSize;
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player.transform);
    }
}
