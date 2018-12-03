using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Logics/Set Tile Logic", order = 3)]
public class SetTileLogic : ActionLogic
{
    [SerializeField]
    private TileBase _newTile;

    [SerializeField]
    private Vector3Int _currentTilePosition;

    [SerializeField]
    private bool _keepCollision = true;

    public override void Execute()
    {
        GameManager.Instance.GameEvents.SetVisualTile(_currentTilePosition, _newTile);
        if (!_keepCollision)
        {
            GameManager.Instance.GameEvents.RemoveTileCollision(_currentTilePosition);
        }
        Finish();
    }

    public override void Finish()
    {
        _callNext();
    }
}
