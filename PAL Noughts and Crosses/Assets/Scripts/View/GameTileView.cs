using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTileView : MonoBehaviour
{
    public System.Action OnTileClicked;

    [SerializeField]
    private GameObject _noughtImage;

    [SerializeField]
    private GameObject _crossImage;

    [SerializeField]
    private Color _gameplayColour;

    [SerializeField]
    private Color _winningLineColour;

    [SerializeField]
    private Image _buttonImage;

    [SerializeField]
    private Button _button;

    public void OnButtonClick()
    {
        OnTileClicked?.Invoke();
    }

    public void Setup(GameTileModel gameTileModel)
    {
        if(gameTileModel.IsWinningTile)
        {
            _buttonImage.color = _winningLineColour;
        }
        else
        {
            _buttonImage.color = _gameplayColour;
        }

        // Set the image, if the button is interactable
        switch (gameTileModel.Tile)
        {
            case BoardTile.Cross:
                _noughtImage.SetActive(false);
                _crossImage.SetActive(true);

                _button.interactable = false;
                break;
            case BoardTile.Nought:
                _noughtImage.SetActive(true);
                _crossImage.SetActive(false);

                _button.interactable = false;
                break;
            case BoardTile.Empty:
                _noughtImage.SetActive(false);
                _crossImage.SetActive(false);

                _button.interactable = true;
                break;
        }
    }
}
