using CommandPattern;
using Hex.Model;
using System.Collections;
using System.Collections.Generic;

namespace Hex.Model.HexEngine.Commands
{
    public class FlippSinglePiece : ICommand
    {
        private readonly Board _board;

        private readonly Tile _tile;


        public FlippSinglePiece(Board board, Tile tile)
        {
            _board = board;
            _tile = tile;
        }

        public void Execute()
        {
            _board.FlippPiece(_tile);
        }

        public void Undo()
        {
            _board.FlippBackPiece(_tile);
        }

        public void Redo()
        {
            _board.FlippPiece(_tile);
        }
    }
}
