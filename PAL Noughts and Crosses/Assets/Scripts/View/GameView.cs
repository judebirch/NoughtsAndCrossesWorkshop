using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public System.Action OnGameRestartClicked;

    [SerializeField]
    private Button _restartButton;

    public void EnableRestartButton(bool enable)
    {
        _restartButton.interactable = enable;
    }

    public void GameRestartClicked()
    {
        OnGameRestartClicked?.Invoke();
    }

    [SerializeField]
    private GameBoardView _gameBoard;

    public GameBoardView GameBoard
    {
        get
        {
            return _gameBoard;
        }
    }

    [SerializeField]
    private TMP_Text _statusText;

    public void SetStatusText(string text)
    {
        _statusText.SetText(text);
    }
}
