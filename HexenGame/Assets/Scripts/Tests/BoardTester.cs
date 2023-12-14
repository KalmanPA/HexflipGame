using Hex.Model;
using Hex.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.Tests
{


    public class BoardTester : MonoBehaviour
    {
        private Board _boardModel;
        // Start is called before the first frame update
        void Start()
        {
            BoardView boardView = FindObjectOfType<BoardView>();
            _boardModel = boardView.BoardModel;

            _boardModel.SpawnPiece(new GridPosition(0, 0, 0), CurrentColor.White);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _boardModel.RemovePiece(new GridPosition(0, 0, 0));
            }
        }
    }
}
