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



    public PlayerView(CurrentColor playerColor, VisualElement playerColorName)
    {
        _playerColor = playerColor;
        _playerPanel = playerColorName;

        playerColorName.Q<Label>().text = playerColor.ToString();

        //playerPanel.Q<Label>().set

        //_playerPanel.Q

        //_piecesListView = playerPanel.Q<ListView>("captured_pieces");
    }

    //public void AddCapturedPiece(string pieceName)
    //{
    //    _piecesListView.hierarchy.Add(new Label(pieceName));
    //}



    public void SetHighlighted(bool isHighlighted)
    {
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
