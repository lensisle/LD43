using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameEventSystem : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField]
    private Tilemap _visualInteractiveTilemap;

    private List<GameAction> _actions;
    public List<GameAction> Actions
    {
        get
        {
            return _actions;
        }
    }

    private Queue<GameAction> _activeActions;
    Dictionary<string, int> _variables;

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
        _variables = new Dictionary<string, int>();

        GameAction[] actions = GetComponentsInChildren<GameAction>(true);
        _actions = new List<GameAction>(actions);
        foreach (GameAction action in _actions)
        {
            Vector3 localPosition = action.TilePos;
            Vector3Int cellPos = _tilemap.LocalToCell(localPosition);
            TileBase tileBase = _tilemap.GetTile(cellPos);
            action.SetTile(tileBase);
            action.Initialize();

            if (string.IsNullOrEmpty(action.ActionCondition.ID) == false && action.ActionCondition.TargetVarQuantity > 0)
            {
                _variables.Add(action.ActionCondition.ID, 0);
            }
        }
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

        GameAction action = _activeActions.Dequeue();

        if (action.IsComplete)
        {
            action.StartAction(EActionInitializationMode.Completed);
            return;
        }

        bool conditionExist = action.ActionCondition != null && !string.IsNullOrEmpty(action.ActionCondition.ID);

        if (conditionExist)
        {
            int varValue;
            bool valueExist = _variables.TryGetValue(action.ActionCondition.ID, out varValue);

            // if the current value is more than the static value we call the normal flow
            if (valueExist && varValue >= action.ActionCondition.TargetVarQuantity)
            {
                action.StartAction(EActionInitializationMode.Normal);
            }
            else 
            {
                // otherwise we execute the fallback flow
                action.StartAction(EActionInitializationMode.Fallback);
            }
        }
        else 
        {
            // if a condition does not exist we just execute the normal flow
            action.StartAction(EActionInitializationMode.Normal);
        }
    }

    public void NotifyEndAction()
    {
        CheckForActions();
    }

    public Vector3Int GetCellPositionFromLocal(Vector3 localPosition)
    {
        return _tilemap.LocalToCell(localPosition);
    }

    public Vector3Int GetCellPlayerPosition(Vector3 worldPosition)
    {
        Vector3 localPos = _tilemap.WorldToLocal(worldPosition);
        return _tilemap.LocalToCell(localPos);
    }

    public Vector3 GetLocalPlayerPosition(Vector3 worldPosition)
    {
        return _tilemap.WorldToLocal(worldPosition);
    }

    public void CheckNearPressButtonAction(Vector3 playerPos)
    {
        Vector3Int cellPlayerPos = GetCellPlayerPosition(playerPos);

        foreach (GameAction action in _actions)
        {
            Vector3Int cellPos = GetCellPositionFromLocal(action.TilePos);
            bool valid = false;

            if (cellPos.x - 1 == cellPlayerPos.x && cellPos.y == cellPlayerPos.y ||
                cellPos.x + 1 == cellPlayerPos.x && cellPos.y == cellPlayerPos.y ||
                cellPos.y - 1 == cellPlayerPos.y && cellPos.x == cellPlayerPos.x ||
                cellPos.y + 1 == cellPlayerPos.y && cellPos.x == cellPlayerPos.x)
            {
                valid = true;
            }

            if (action.ActionTrigger == EActionTrigger.NearPress && valid)
            {
                AppendAction(action);
            }
        }
    }

    public void CheckOverPress(Vector3 playerPos)
    {
        Vector3Int cellPlayerPos = GetCellPlayerPosition(playerPos);

        foreach (GameAction action in _actions)
        {
            Vector3Int cellPos = GetCellPositionFromLocal(action.TilePos);
            bool valid = cellPos.x == cellPlayerPos.x && cellPos.y == cellPlayerPos.y;

            if (action.ActionTrigger == EActionTrigger.OverPress && valid)
            {
                AppendAction(action);
            }
        }
    }

    public void SetVariableValue(string id, int value)
    {
        if (_variables.ContainsKey(id))
        {
            _variables[id] = value;
        }
        else 
        {
            Debug.LogError("Variable: " + id + " does not exist");
        }
    }

    public void SetVisualTile(Vector3Int gridPosition, TileBase newTile)
    {
        _visualInteractiveTilemap.SetTile(gridPosition, newTile);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_tilemap == null) return;

        foreach (var pos in _tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            GameObject target = GameObject.Find(localPlace.ToString());

            if (_tilemap.HasTile(localPlace) && target == null)
            {
                GameObject dataTile = new GameObject();
                dataTile.name = localPlace.ToString();
                dataTile.transform.SetParent(_tilemap.transform);
                GameAction action = dataTile.AddComponent<GameAction>();
                dataTile.transform.position = _tilemap.CellToWorld(localPlace);
                _tilemap.GetTile(localPlace);
                action.SetTilePos(pos);

            }
            else if (_tilemap.HasTile(localPlace) == false && target != null)
            {
                DestroyImmediate(target);
            }
        }
    }
#endif
}
