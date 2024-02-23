using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardView : MonoBehaviour
{
    [SerializeField]
    private GameTileView[] _tiles;

    public System.Action<int> OnTileClickedAction;

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            int index = i;
            _tiles[index].OnTileClicked += () => OnTileClickedAction.Invoke(index);
        }
    }

    public void SetupView(GameBoardModel boardModel)
    {
        for (int i = 0; i < 9; i++)
        {
            _tiles[i].Setup(boardModel.Tiles[i]);
        }
    }

    public void SetTile(int tile, GameTileModel boardTile)
    {
        _tiles[tile].Setup(boardTile);
    }
}
