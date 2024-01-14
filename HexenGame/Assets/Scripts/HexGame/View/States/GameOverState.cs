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
        private PauseUI _pauseUI;

        //private HexGame _hexGame;

        public GameOverState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }
        public void OnEnter()
        {
            _pauseUI = GameObject.FindObjectOfType<PauseUI>();
            //_hexGame = GameObject.FindObjectOfType<HexGame>();

            _pauseUI.MenuClicked += PauseUI_MenuClicked;
            _pauseUI.NewGameClicked += PauseUI_NewGameClicked;

            //_hexGame.GameOver += _hexGame_GameOver;

            _pauseUI.Show();
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
            _pauseUI.MenuClicked -= PauseUI_NewGameClicked;
            _pauseUI.NewGameClicked -= PauseUI_MenuClicked;
            _pauseUI?.Hide();
        }

        public void Update()
        {
            
        }
    }
}
