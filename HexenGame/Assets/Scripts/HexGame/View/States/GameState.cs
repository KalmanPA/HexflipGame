
using Hex.View;
using StatePattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hex.View.States
{
    public class GameState : IState
    {
        private readonly GameStateSystem _stateSystem;

        private HexGame _hexGame;

        //public static bool IsGameOver;
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

            _hexGame = GameObject.FindObjectOfType<HexGame>();

            _hexGame.GameOver += HexGame_GameOver;

        }

        private void HexGame_GameOver(object sender, System.EventArgs e)
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.GameOverState);
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
