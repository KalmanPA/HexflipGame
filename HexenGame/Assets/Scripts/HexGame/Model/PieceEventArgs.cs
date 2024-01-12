using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
namespace Hex.Model
{
    public class PieceEventArgs : EventArgs
    {
        public Piece Piece { get; }
        public PieceEventArgs(Piece piece)
        {
            Piece = piece;
        }
    }
}
