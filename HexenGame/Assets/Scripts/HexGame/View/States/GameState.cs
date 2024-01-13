
using Hex.View;
using StatePattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hex.View.States
{
    public class GameState : IState
    {
        private readonly GameStateSystem _stateSystem;

        public static bool IsGameOver;
        public GameState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                SceneManager.LoadScene("GameScene");
            }

        }

        public void OnExit()
        {

        }

        public void Update()
        {
            if (!IsGameOver)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _stateSystem.StateMachine.MoveToState(_stateSystem.PauseState);
                }
            }
            else
            {
                _stateSystem.StateMachine.MoveToState(_stateSystem.PauseState);
            }


        }
    }
}
