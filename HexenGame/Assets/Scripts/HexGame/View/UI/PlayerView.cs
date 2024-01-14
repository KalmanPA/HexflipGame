using Hex.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerView
{
    private readonly CurrentColor _playerColor;
    private readonly VisualElement _playerPanel;
    //private ListView _piecesListView;

    private readonly int _numberOfStartingBlackPieces = 4;
    private readonly int _numberOfStartingWhitePieces = 3;

    public PlayerView(CurrentColor playerColor, VisualElement playerPanel)
    {
        _playerColor = playerColor;
        _playerPanel = playerPanel;

        playerPanel.Q<Label>("Name").text = playerColor.ToString();

        if (playerColor == CurrentColor.White)
        {
            playerPanel.Q<Label>("Number").text = $"Score: {_numberOfStartingWhitePieces}";
        }

        if (playerColor == CurrentColor.Black)
        {
            playerPanel.Q<Label>("Number").text = $"Score: {_numberOfStartingBlackPieces}";
        }


        //playerPanel.Q<Label>().set

        //_playerPanel.Q

        //_piecesListView = playerPanel.Q<ListView>("captured_pieces");
    }

    //public void AddCapturedPiece(string pieceName)
    //{
    //    _piecesListView.hierarchy.Add(new Label(pieceName));
    //}



    public void SetHighlighted(bool isHighlighted, int numberOfPieces)
    {
        _playerPanel.Q<Label>("Number").text = $"Score: {numberOfPieces}";

        string activeClassName = "activePlayer";

        if (isHighlighted)
        {
            _playerPanel.AddToClassList(activeClassName);
        }
        else
        {
            _playerPanel.RemoveFromClassList(activeClassName);
        }
    }
}
