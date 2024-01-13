using Hex.View.UI;
using StatePattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hex.View.States
{
    public class MenuState : IState
    {
        private readonly GameStateSystem _stateSystem;
        public MenuState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }



        public void OnEnter()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Menu");
            loadOperation.completed += LoadOperation_completed;
        }

        private void LoadOperation_completed(AsyncOperation operation)
        {
            MenuUI menuUI = GameObject.FindObjectOfType<MenuUI>();
            menuUI.NewGameClicked += MenuUI_NewGameClicked;
        }

        private void MenuUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {
            MenuUI menuUI = GameObject.FindObjectOfType<MenuUI>();

            if (menuUI != null )
            {
                menuUI.NewGameClicked -= MenuUI_NewGameClicked;
            }
            
        }

        public void Update()
        {

        }
    }
}