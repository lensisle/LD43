using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour, IGameManagerDependency
{
    private Queue<GameAction> _activeActions;

    public bool IsBusy 
    {
        get 
        {
            return _activeActions == null || _activeActions.Count > 0;
        }
    }

    public void Initialize()
    {
        _activeActions = new Queue<GameAction>();
    }

    public void AppendAction(GameAction action)
    {
        _activeActions.Enqueue(action);
        CheckForActions();
    }

    private void CheckForActions()
    {
        if (_activeActions.Count < 1)
        {
            return;
        }

        _activeActions.Dequeue().StartAction();
    }

    public void NotifyEndAction()
    {
        CheckForActions();
    }
}
