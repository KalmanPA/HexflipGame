using Hex.View.States;
using Hex.View.UI;
using Hex.Model.HexEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex.Model;
using System;

namespace Hex.View
{

    public class HexGame : MonoBehaviour
    {
        public event EventHandler GameOver;


        public int NumberOfWhitePieces;
        public int NumberOfBlackPieces;


        //public static bool IsGameOver;

        [SerializeField] private PauseUI _pauseUI;
        //[SerializeField] private GameplayUI _gameplayUI;

        public event EventHandler PlayerColorChanged;

        public CurrentColor CurrentPlayerColor;

        private HexEngine _hexEngine;
        void Start()
        {
            CurrentPlayerColor = CurrentColor.White;

            BoardView bv = FindAnyObjectByType<BoardView>();
            _hexEngine = new HexEngine(bv.BoardModel, Model.CurrentColor.White);

            _hexEngine.FindValidTiles();

            //_hexEngine.CountNumberOfPieces();

            //NumberOfWhitePieces = _hexEngine.NumberOfWhitePieces;

            //NumberOfBlackPieces = _hexEngine.NumberOfBlackPieces;

            _hexEngine.GameOver += HexEngine_GameOver;

            _hexEngine.PlayerColorChanged += HexEngine_PlayerColorChanged;

            //_pauseUI.MenuTitleText = "Game Over";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _hexEngine.Undo();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _hexEngine.Redo();
            }
        }

        private void HexEngine_PlayerColorChanged(object sender, System.EventArgs e)
        {
            CurrentPlayerColor = _hexEngine.CurrentColor;

            NumberOfWhitePieces = _hexEngine.NumberOfWhitePieces;

            NumberOfBlackPieces = _hexEngine.NumberOfBlackPieces;

            PlayerColorChanged?.Invoke(this, EventArgs.Empty);
        }

        private void HexEngine_GameOver(object sender, System.EventArgs e)
        {
            _pauseUI.MenuTitleText = "Game Over";

            if (_hexEngine.WinnerColor == CurrentColor.White)
            {
                _pauseUI.WinText = "White wins";
            }
            else
            {
                _pauseUI.WinText = "Black wins";
            }

            //IsGameOver = true;

            GameOver?.Invoke(this, EventArgs.Empty);

            //GameState.IsGameOver = true;

            //PauseState.IsGameOver = true;
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        _hexEngine.ValidPositions.Clear();
        //    }

        //}
    }
}
