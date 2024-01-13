using Hex.View.UI;
using Hex.View;
using StatePattern;
using UnityEngine.SceneManagement;

namespace Hex.View.States
{
    public class NewGameState : IState
    {
        private readonly GameStateSystem _stateSystem;
        private PauseUI _pauseUI;
        public NewGameState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            //HexGame.IsGameOver = false;

            PauseState.IsGameOver = false;
            GameState.IsGameOver = false;

            var loadOperation = SceneManager.LoadSceneAsync("GameScene");
            loadOperation.completed += (op) =>
            {
                _stateSystem.StateMachine.MoveToState(_stateSystem.GameState);
            };


        }

        public void OnExit()
        {

        }

        public void Update()
        {

        }
    }
}
