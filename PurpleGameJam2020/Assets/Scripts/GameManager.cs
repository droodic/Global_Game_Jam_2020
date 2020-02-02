using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] float roundTime;
    bool playing = true;

    public bool Playing { get => playing; set => playing = value; }
    public float RoundTime { get => roundTime; set => roundTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing && RoundTime > 0)
        {
            RoundTime -= Time.deltaTime;
        }
        else if(Playing && RoundTime < 0)
        {
            playing = false;
            UIManager.Instance.DisplayVictory();
        }
    }
}
