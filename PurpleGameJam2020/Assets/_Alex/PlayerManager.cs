using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private List<Player> _players;
    public List<Player> Players { get => _players; set => _players = value; }

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        switch (Gamepad.all.Count)
        {
            case 0:
                _playerInputManager.JoinPlayer();
                _playerInputManager.JoinPlayer();
                break;
            case 1:
                _playerInputManager.JoinPlayer();
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[0]);
                break;
            case 2:
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[0]);
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[1]);
                break;
            default:
                break;
        }
    }
    public void AddPlayer(Player player)
    {
        _players.Add(player);
        if (_players.Count ==1)
        {
            UIManager.Instance.P1 = player;
            UIManager.Instance.P1.tag = "Player";
        }
        else if (_players.Count == 2)
        {
            UIManager.Instance.P2 = player;
            UIManager.Instance.P2.tag = "Player2";
        }
    }
}
