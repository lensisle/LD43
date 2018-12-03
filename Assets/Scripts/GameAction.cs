using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public enum EActionTrigger
{
    Contact,
    Over,
    PressButton,
    Var
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

    public void SetTilePos(Vector3 tilePos)
    {
        _tilePos = tilePos;
    }

    public void SetTile(TileBase tile)
    {
        _tile = tile;
    }

    public void StartAction()
    {
        _activeLogics = new Queue<ActionLogic>(_logics);
        CallActiveLogic();
    }

    public void CallActiveLogic() 
    {
        if (_activeLogics.Count < 1) 
        {
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
