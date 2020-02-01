using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }
}
