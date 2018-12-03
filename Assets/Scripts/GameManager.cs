using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string ManagerGoName = "GameManager";

    private static GameManager _instance;
    public static GameManager Instance 
    {
        get 
        {
            if (_instance != null)
            {   
                return _instance;
            }

            GameObject gameManagerGO = GameObject.Find(ManagerGoName);
            if (gameManagerGO == null)
            {
                gameManagerGO = new GameObject(ManagerGoName);
            }

            _instance = gameManagerGO.GetComponent<GameManager>();
            if (_instance == null)
            {
                _instance = gameManagerGO.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    [SerializeField]
    private CustomCamera _camera;
    public CustomCamera GameCamera 
    {
        get 
        {
            return _camera;
        }
    }

    [SerializeField]
    private PlayerComp _player;
    public PlayerComp Player
    {
        get
        {
            return _player;
        }
    }

    [SerializeField]
    private UIManager _ui;
    public UIManager UI 
    {
        get 
        {
            return _ui;
        }
    }

    [SerializeField]
    private GameEventSystem _gameEvents;
    public GameEventSystem GameEvents 
    {
        get 
        {
            return _gameEvents;
        }
    }

	void Start ()
    {
        _gameEvents.Initialize();
        _ui.Initialize();
        _player.Initialize();
        _camera.Initialize();

        _camera.FollowTarget(_player.Transform);
	}
	
	void Update ()
    {
        if (_gameEvents.IsBusy && _player.IsAllowingMovement) 
        {
            _player.SetAllowMovements(false);
        }
    }
}
