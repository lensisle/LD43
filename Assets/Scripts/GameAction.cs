using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

public enum EActionTrigger
{
    Contact,
    OverPress,
    NearPress
}

public enum EActionInitializationMode 
{
    Fallback,
    Normal,
    Completed
}

[Serializable]
public class ActionCondition
{
    public string ID;
    public int TargetVarQuantity;
}

public class GameAction : MonoBehaviour
{
    [SerializeField]
    private EActionTrigger _actionTrigger;
    public EActionTrigger ActionTrigger 
    {
        get 
        {
            return _actionTrigger;
        }
    }

    [SerializeField]
    private List<ActionLogic> _logics;

    [SerializeField]
    private List<ActionLogic> _fallbackLogics;

    [SerializeField]
    private List<ActionLogic> _completedLogics;

    [SerializeField]
    private ActionCondition _actionCondition;
    public ActionCondition ActionCondition 
    {
        get 
        {
            return _actionCondition;
        }
    }

    [SerializeField]
    private bool _keepCompletedState;

    private bool _isComplete;
    public bool IsComplete 
    {
        get 
        {
            return _isComplete;
        }
    }

    private Queue<ActionLogic> _activeLogics;

    [SerializeField]
    private Vector3 _tilePos;
    public Vector3 TilePos
    {
        get 
        {
            return _tilePos;
        }
    }

    private TileBase _tile;
    public TileBase Tile 
    {
        get 
        {
            return _tile;
        }
    }

    private EActionInitializationMode _currentMode;

    public void Initialize()
    {
        _isComplete = false;
    }

    public void SetTilePos(Vector3 tilePos)
    {
        _tilePos = tilePos;
    }

    public void SetTile(TileBase tile)
    {
        _tile = tile;
    }

    public void StartAction(EActionInitializationMode mode)
    {
        if (mode == EActionInitializationMode.Normal)
        {
            _activeLogics = new Queue<ActionLogic>(_logics);
            _currentMode = EActionInitializationMode.Normal;
        }
        else if (mode == EActionInitializationMode.Fallback)
        {
            _activeLogics = new Queue<ActionLogic>(_fallbackLogics);
            _currentMode = EActionInitializationMode.Fallback;
        }
        else if (mode == EActionInitializationMode.Completed)
        {
            _activeLogics = new Queue<ActionLogic>(_completedLogics);
            _currentMode = EActionInitializationMode.Completed;
        }

        CallActiveLogic();
    }

    public void CallActiveLogic() 
    {
        if (_activeLogics.Count < 1) 
        {
            // we set complete state if it's normal with condition or 
            // if it's fallback type and no conditions exist
            if ((_keepCompletedState && _currentMode == EActionInitializationMode.Normal) ||
                (_keepCompletedState && _currentMode == EActionInitializationMode.Fallback && string.IsNullOrEmpty(_actionCondition.ID)))
            {
                _isComplete = true;
            }

            Finish();
        }
        else 
        {
            ActionLogic logic = _activeLogics.Dequeue();
            logic.PrepareLogic(CallActiveLogic);
            logic.Execute();
        }
    }

    void Finish()
    {
        GameManager.Instance.GameEvents.NotifyEndAction();
    }
}
