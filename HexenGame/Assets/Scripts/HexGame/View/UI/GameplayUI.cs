using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hex.View.UI
{


    public class GameplayUI : MonoBehaviour
    {
        private PlayerView _player1Panel, _player2Panel;

        private CurrentColor _currentColorOfPlayer;

        [SerializeField] private HexGame _hexGame;

        //public CurrentColor CurrentColorOfPlayer
        //{
        //    get => currentColorOfPlayer; set
        //    {
        //        currentColorOfPlayer = value;
        //    }
        //}


        // Start is called before the first frame update
        void Awake()
        {
            _hexGame.PlayerColorChanged += _hexGame_PlayerColorChanged;

            UIDocument document = GetComponent<UIDocument>();
            VisualElement rootVisual = document.rootVisualElement;

            _player1Panel = new PlayerView(CurrentColor.White, rootVisual.Q("Player1"));
            _player2Panel = new PlayerView(CurrentColor.Black, rootVisual.Q("Player2"));
        }

        private void _hexGame_PlayerColorChanged(object sender, EventArgs e)
        {
            if (_hexGame.CurrentPlayerColor == CurrentColor.White)
            {
                _player1Panel.SetHighlighted(true);
                _player2Panel.SetHighlighted(false);
            }
            else
            {
                _player1Panel.SetHighlighted(false);
                _player2Panel.SetHighlighted(true);
            }
        }

        void Start()
        {
            //Board.Instance.PlayerChanged += OnPlayerChanged;
            _player2Panel.SetHighlighted(false);
            //OnPlayerChanged(Board.Instance, EventArgs.Empty);
        }

        //public void CapturedPiece(Piece piece)
        //{
        //    PlayerView playerView = piece.Player == Player.White ? _player2Panel : _player1Panel;
        //    string pieceName = piece.GetType().Name;
        //    playerView.AddCapturedPiece(pieceName);
        //}


        //private void OnPlayerChanged(object sender, System.EventArgs e)
        //{
        //    if (Board.Instance._currentPlayerTeam == Player.White)
        //    {
        //        _player1Panel.SetHighlighted(true);
        //        _player2Panel.SetHighlighted(false);
        //    }
        //    else
        //    {
        //        _player1Panel.SetHighlighted(false);
        //        _player2Panel.SetHighlighted(true);
        //    }
        //}
    }
}
