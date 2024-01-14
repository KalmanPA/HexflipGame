using System.Collections;
using System.Collections.Generic;
using CommandPattern;

namespace Hex.Model.HexEngine.Commands
{


    public class PlacePieceCommand : ICommand
    {

        private readonly Board _board;
        private readonly GridPosition _pos;
        private readonly CurrentColor _currentPlayerColor;


        public PlacePieceCommand(Board board, GridPosition pos, CurrentColor currentPlayerColor)
        {
            _board = board;
            _pos = pos;
            _currentPlayerColor = currentPlayerColor;
        }

        public void Execute()
        {
            _board.SpawnPiece(_pos, _currentPlayerColor);
        }

        public void Undo()
        {
            _board.RemovePiece(_pos);
        }

        public void Redo()
        {
            _board.SpawnPiece(_pos, _currentPlayerColor);
        }
    }

}
