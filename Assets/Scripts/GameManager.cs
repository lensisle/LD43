using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string MANAGER_GO_NAME = "GameManager";

    private static GameManager _instance;
    public static GameManager Instance 
    {
        get 
        {
            if (_instance != null)
            {   
                return _instance;
            }

            GameObject gameManagerGO = GameObject.Find(MANAGER_GO_NAME);
            if (gameManagerGO == null)
            {
                gameManagerGO = new GameObject(MANAGER_GO_NAME);
                gameManagerGO.name = MANAGER_GO_NAME;
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

    [SerializeField]
    private ActionsData _actionsData;
    public ActionsData ActionsData 
    {
        get 
        {
            return _actionsData;
        }
    }

	void Start ()
    {
        _gameEvents.Initialize();
        _ui.Initialize();
        _player.Initialize();
        _camera.Initialize();
        _actionsData.Initialize();

        _camera.FollowTarget(_player.Transform);

        _ui.AppendDialogue("weird voice in your head", "[SPACE] to talk");
        _ui.AppendDialogue("weird voice in your head", "reality, fiction. does it matter?");
        _ui.AppendDialogue("weird voice in your head", "everything is going to dissapear anyways.");
    }
	
	void Update ()
    {
        if (_gameEvents.IsBusy && _player.IsAllowingMovement) 
        {
            _player.SetAllowMovements(false);
        }

        if (_ui.IsBusy && _player.IsAllowingMovement)
        {
            _player.SetAllowMovements(false);
        }

        if ((_gameEvents.IsBusy == false && _ui.IsBusy == false) && _player.IsAllowingMovement == false)
        {
            _player.SetAllowMovements(true);
        }
    }

    public void CheckPressButtonAction(Vector3 playerPos)
    {
        foreach(GameAction action in _actionsData.Actions)
        {
            if (action.ActionTrigger == EActionTrigger.PressButton && (_actionsData.GetCellPlayerPosition(playerPos) == action.TilePos))
            {
                _gameEvents.AppendAction(action);
            }
        }
    }
}
