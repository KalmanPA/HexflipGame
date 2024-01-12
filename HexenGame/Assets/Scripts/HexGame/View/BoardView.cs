using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View
{

    public class BoardView : MonoBehaviour
    {
        [SerializeField]
        private PieceView _piecePrefab;
        public Board BoardModel { get; private set; }//We create the BoardModel
        private void Awake()
        {
            BoardModel = new Board();//We create the BoardModel
            BoardModel.PieceSpawned += Board_ModelPieceSpawned;

            TileView[] childTiles = GetComponentsInChildren<TileView>();//Get all the TileViews from the in engine board
            foreach (var tileView in childTiles)
            {
                GridPosition position = PositionHelper.WorldToGrid(tileView.transform.localPosition);//Get GridPosition
                Tile tileModel = BoardModel.AddTile(position);//Adds tile and position to the Dictionary in the Board

                tileView.SetModel(tileModel);//Assigns the TileModel to the TileView
            }
        }

        private void Board_ModelPieceSpawned(object sender, PieceEventArgs e)
        {
            Piece pieceModel = e.Piece;
            PieceView prefab = _piecePrefab;

            Vector3 worldPos = PositionHelper.GridToWorld(pieceModel.GridPosition);
            Quaternion rot = Quaternion.identity;

            GameObject spawnedPieceObject = GameObject.Instantiate(prefab.gameObject,
                PositionHelper.GridToWorld(pieceModel.GridPosition), rot);

            spawnedPieceObject.GetComponent<PieceView>().SetModel(pieceModel);
        }
    }
}
