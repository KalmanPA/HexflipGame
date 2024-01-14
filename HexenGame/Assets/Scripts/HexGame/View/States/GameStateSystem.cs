
using StatePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hex.View.States
{
    public class GameStateSystem : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }
        public MenuState MenuState { get; private set; }

        public GameState GameState { get; private set; }

        public PauseState PauseState { get; private set; }
        public NewGameState NewGameState { get; private set; }

        public GameOverState GameOverState { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            //Create all states
            MenuState = new MenuState(this);

            GameState = new GameState(this);

            PauseState = new PauseState(this);

            NewGameState = new NewGameState(this);

            GameOverState = new GameOverState(this);

            StateMachine = new StateMachine(MenuState);
        }

        private void Update()
        {
            StateMachine.Update();
        }
    }
}
