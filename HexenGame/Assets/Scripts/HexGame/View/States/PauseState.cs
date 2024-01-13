using Hex.View.UI;
using Hex.View;
using StatePattern;
using System;
using UnityEngine;

namespace Hex.View.States
{
    public class PauseState : IState
    {
        public static bool IsGameOver;

        private readonly GameStateSystem _stateSystem;
        private PauseUI _pauseUI;
        public PauseState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            _pauseUI = GameObject.FindObjectOfType<PauseUI>();

            _pauseUI.MenuClicked += _pauseUI_MenuClicked;
            _pauseUI.NewGameClicked += _pauseUI_NewGameClicked;

            _pauseUI.Show();
        }

        private void _pauseUI_MenuClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.MenuState);
        }

        private void _pauseUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {

            _pauseUI.MenuClicked -= _pauseUI_NewGameClicked;
            _pauseUI.NewGameClicked -= _pauseUI_MenuClicked;
            _pauseUI?.Hide();
        }

        public void Update()
        {
            if (!IsGameOver)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _stateSystem.StateMachine.MoveToState(_stateSystem.GameState);
                }
            }
            
        }
    }
}
