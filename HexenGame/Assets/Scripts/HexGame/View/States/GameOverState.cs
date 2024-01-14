using Hex.View.UI;
using StatePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View.States
{
    public class GameOverState : IState
    {
        private readonly GameStateSystem _stateSystem;
        private GameOverUI _gameOverUI;

        //private HexGame _hexGame;

        public GameOverState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }
        public void OnEnter()
        {
            _gameOverUI = GameObject.FindObjectOfType<GameOverUI>();
            //_hexGame = GameObject.FindObjectOfType<HexGame>();

            _gameOverUI.MenuClicked += PauseUI_MenuClicked;
            _gameOverUI.NewGameClicked += PauseUI_NewGameClicked;

            //_hexGame.GameOver += _hexGame_GameOver;

            _gameOverUI.Show();
        }

        

        private void PauseUI_MenuClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.MenuState);
        }

        private void PauseUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {
            _gameOverUI.MenuClicked -= PauseUI_NewGameClicked;
            _gameOverUI.NewGameClicked -= PauseUI_MenuClicked;
            _gameOverUI?.Hide();
        }

        public void Update()
        {
            
        }
    }
}
