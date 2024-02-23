using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileModel
{
    public GameTileModel(BoardTile boardTile)
    {
        _tile = boardTile;
    }

    private BoardTile _tile;
    public BoardTile Tile
    {
        get
        {
            return _tile;
        }
        set
        {
            _tile = value;
        }
    }

    private bool _isWinningTile;
    public bool IsWinningTile
    {
        get
        {
            return _isWinningTile;
        }
        set
        {
            _isWinningTile = value;
        }
    }
}

public enum BoardTile
{
    Empty,
    Nought,
    Cross
}