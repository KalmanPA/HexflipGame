
using StatePattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chess.View.States
{
    public class GameState : IState
    {
        private readonly GameStateSystem _stateSystem;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _stateSystem.StateMachine.MoveToState(_stateSystem.PauseState);
            }
        }
    }
}
