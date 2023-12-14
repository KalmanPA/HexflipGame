using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View
{
    public class PieceView : MonoBehaviour
    {
        //[SerializeField] GameObject _piecePrefab;
        public Piece PieceModel { get; private set; }

        public void SetModel(Piece piece)
        {
            PieceModel = piece;
            piece.Flipped += Piece_IsFlipped;
            piece.Removed += Piece_Removed;

            if (piece.CurrentColor == CurrentColor.Black)
            {
                Piece_IsFlipped(this, EventArgs.Empty);
            }
        }

        private void Piece_IsFlipped(object sender, EventArgs e)
        {
            transform.Rotate(180f, 0f, 0f);
        }

        private void Piece_Removed(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            PieceModel.Flipped -= Piece_IsFlipped;
            PieceModel.Removed -= Piece_Removed;
        }
    }
}
