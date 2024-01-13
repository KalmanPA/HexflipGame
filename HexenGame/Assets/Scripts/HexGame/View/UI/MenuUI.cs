using System.Collections;
using System.Collections.Generic;
//using Unity.Plastic.Newtonsoft.Json.Serialization;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hex.View.UI
{
    public class MenuUI : MonoBehaviour
    {
        public event Action NewGameClicked;

        private void Awake()
        {
            UIDocument document = GetComponent<UIDocument>();

            Button newGameButton = document.rootVisualElement.Q<Button>("NewGameButton");
            newGameButton.clicked += NewGameButton_clicked;
        }

        private void NewGameButton_clicked()
        {
            NewGameClicked?.Invoke();
        }
    }
}
