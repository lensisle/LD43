using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public class ActionsData : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private Tilemap _tilemap;

    private List<GameAction> _actions;
    public List<GameAction> Actions 
    {
        get 
        {
            return _actions;
        }
    }

    public void Initialize()
    {
        GameAction[] actions = GetComponentsInChildren<GameAction>(true);
        _actions = new List<GameAction>(actions);
        foreach(GameAction action in _actions)
        {
            Vector3 localPosition = action.TilePos;
            Vector3Int cellPos = _tilemap.LocalToCell(localPosition);
            TileBase tileBase = _tilemap.GetTile(cellPos);
            action.SetTile(tileBase);
        }
    }

    public bool checkTileAndPosition(Vector3 worldPositon, TileBase targetTile)
    {
        Vector3 localPos = _tilemap.WorldToLocal(worldPositon);
        Vector3Int cellPos = _tilemap.LocalToCell(localPos);
        TileBase tile = _tilemap.GetTile(cellPos);

        if (tile == null) 
        {
            return false;
        }

        return targetTile == tile;
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
                TileBase tile = _tilemap.GetTile(localPlace);
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
