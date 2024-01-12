using Chess.View.UI;
using StatePattern;
using UnityEngine.SceneManagement;

namespace Chess.View.States
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
