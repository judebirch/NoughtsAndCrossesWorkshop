using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardModel
{
    public GameBoardModel()
    {
        _tiles = new GameTileModel[9];

        for (int i = 0; i < 9; i++)
        {
            _tiles[i] = new GameTileModel(BoardTile.Empty);
        }
    }

    private GameTileModel[] _tiles;

    public GameTileModel[] Tiles
    {
        get
        {
            return _tiles;
        }
    }

    private bool _isComplete = false;

    public bool IsComplete
    {
        get
        {
            return _isComplete;
        }
    }

    public System.Action<int, GameTileModel> OnBoardTileSet;

    public void SetTile(int index, BoardTile boardTile)
    {
        _tiles[index].Tile = boardTile;

        OnBoardTileSet?.Invoke(index, _tiles[index]);

        if(WinDetect(index, boardTile))
        {
            _isComplete = true;
        }
    }

    #region Win Detection
    private List<int> _winningTileIndices;

    public List<int> GetWinningTiles()
    {
        return _winningTileIndices;
    }

    public bool WinDetect(int index, BoardTile boardTile)
    {
        _winningTileIndices = new List<int>();
        _winningTileIndices.Add(index);

        // Upward/ Downwards
        int vertical = MeasureMatchingDist(index, 0, 1, boardTile) + MeasureMatchingDist(index, 0, -1, boardTile) + 1;

        if(vertical == 3)
        {
            _winningTileIndices.AddRange(GetPositionsInDirection(index, 0, 1, boardTile));
            _winningTileIndices.AddRange(GetPositionsInDirection(index, 0, -1, boardTile));
        }

        int horizontal = MeasureMatchingDist(index, 1, 0, boardTile) + MeasureMatchingDist(index, -1, 0, boardTile) + 1;

        if (horizontal == 3)
        {
            _winningTileIndices.AddRange(GetPositionsInDirection(index, 1, 0, boardTile));
            _winningTileIndices.AddRange(GetPositionsInDirection(index, -1, 0, boardTile));
        }

        int upRight = MeasureMatchingDist(index, 1, 1, boardTile) + MeasureMatchingDist(index, -1, -1, boardTile) + 1;

        if (upRight == 3)
        {
            _winningTileIndices.AddRange(GetPositionsInDirection(index, 1, 1, boardTile));
            _winningTileIndices.AddRange(GetPositionsInDirection(index, -1, -1, boardTile));
        }

        int downRight = MeasureMatchingDist(index, 1, -1, boardTile) + MeasureMatchingDist(index, -1, 1, boardTile) + 1;

        if (downRight == 3)
        {
            _winningTileIndices.AddRange(GetPositionsInDirection(index, 1, -1, boardTile));
            _winningTileIndices.AddRange(GetPositionsInDirection(index, -1, 1, boardTile));
        }

        if (Mathf.Max(vertical, horizontal, upRight, downRight) == 3)
        {
            foreach(int winningTile in _winningTileIndices)
            {
                _tiles[winningTile].IsWinningTile = true;
            }

            return true;
        }

        return false;
    }

    private int MeasureMatchingDist(int index, int xOffset, int yOffset, BoardTile boardTile)
    {
        int dist = 1;
        while (GetRelativeIndex(index, dist * xOffset, dist * yOffset) != -1 && _tiles[GetRelativeIndex(index, dist * xOffset, dist * yOffset)].Tile == boardTile)
        {
            dist++;
        }

        return dist - 1;
    }

    private List<int> GetPositionsInDirection(int index, int xOffset, int yOffset, BoardTile boardTile)
    {
        List<int> result = new List<int>();

        int dist = 1;
        while (GetRelativeIndex(index, dist * xOffset, dist * yOffset) != -1)
        {
            result.Add(GetRelativeIndex(index, dist * xOffset, dist * yOffset));

            dist++;
        }

        return result;
    }

    private int GetRelativeIndex(int tileIndex, int xOffset, int yOffset)
    {
        int x = tileIndex % 3 + xOffset;
        int y = tileIndex / 3 + yOffset;

        if(x < 0 || x >= 3 || y < 0 || y >= 3)
        {
            return -1;
        }

        return x + y * 3;
    }
    #endregion
}
