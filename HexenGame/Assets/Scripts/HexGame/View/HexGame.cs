using Hex.Model.HexEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View
{

    public class HexGame : MonoBehaviour
    {
        private HexEngine _hexEngine;
        void Start()
        {
            BoardView bv = FindAnyObjectByType<BoardView>();
            _hexEngine = new HexEngine(bv.BoardModel, Model.CurrentColor.White);

            _hexEngine.FindValidTiles();
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        _hexEngine.ValidPositions.Clear();
        //    }
            
        //}
    }
}
