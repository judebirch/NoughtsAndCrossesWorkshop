using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameView _gameView;

    private GameBoardModel _boardModel;

    public enum GameState
    {
        CrossTurn,
        NoughtsTurn,
        GameComplete
    }

    private GameState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _gameView.GameBoard.OnTileClickedAction += OnTileClicked;
        _gameView.OnGameRestartClicked += OnRestartGameClicked;

        StartNewGame();
    }

    private void StartNewGame()
    {
        _boardModel = new GameBoardModel();
        _gameView.GameBoard.SetupView(_boardModel);

        _gameView.EnableRestartButton(false);

        _boardModel.OnBoardTileSet += _gameView.GameBoard.SetTile;

        SetState(GameState.CrossTurn);
    }

    // Runs everytime the game state changes
    private void SetState(GameState state)
    {
        _currentState = state;

        switch (state)
        {
            case GameState.CrossTurn:
                _gameView.SetStatusText("Cross Turn");
                break;
            case GameState.NoughtsTurn:
                _gameView.SetStatusText("Noughts Turn");
                break;
            case GameState.GameComplete:
                _gameView.GameBoard.SetupView(_boardModel);
                _gameView.EnableRestartButton(true);
                break;
        }
    }

    // This is raised when a board tile in unity has been clicked
    private void OnTileClicked(int tile)
    {
        switch(_currentState)
        {
            case GameState.CrossTurn:
                {
                    _boardModel.SetTile(tile, BoardTile.Cross);

                    if (_boardModel.IsComplete)
                    {
                        // Cross has won
                        _gameView.SetStatusText("Cross Wins!");
                        SetState(GameState.GameComplete);
                    }
                    else
                    {
                        SetState(GameState.NoughtsTurn);
                    }
                }
                break;
            case GameState.NoughtsTurn:
                {
                    _boardModel.SetTile(tile, BoardTile.Nought);

                    if (_boardModel.IsComplete)
                    {
                        // Noughts has won
                        _gameView.SetStatusText("Noughts Wins!");
                        SetState(GameState.GameComplete);
                    }
                    else
                    {
                        SetState(GameState.CrossTurn);
                    }
                }
                break;
            case GameState.GameComplete:
                break;
        }
    }

    private void OnRestartGameClicked()
    {
        StartNewGame();
    }
}