using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chess.View.UI
{
    public class PauseUI : MonoBehaviour
    {
        public event Action NewGameClicked;
        public event Action MenuClicked;

        UIDocument _document;
        private void Awake()
        {
            UIDocument document = GetComponent<UIDocument>();

            Button newGameButton = document.rootVisualElement.Q<Button>("NewGameButton");
            Button menuButton = document.rootVisualElement.Q<Button>("MenuButton");

            newGameButton.clicked += NewGameButton_clicked;
            menuButton.clicked += MenuButton_clicked;

            _document = document;

            Hide();
        }

        private void MenuButton_clicked()
        {
            MenuClicked?.Invoke();
        }

        private void NewGameButton_clicked()
        {
            NewGameClicked?.Invoke();
        }

        public void Show()
        {
            _document.rootVisualElement.visible = true;
        }

        public void Hide()
        {
            _document.rootVisualElement.visible = false;
        }
    }
}
