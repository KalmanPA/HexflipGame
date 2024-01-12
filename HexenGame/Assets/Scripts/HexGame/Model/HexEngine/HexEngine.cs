using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hex.Model.HexEngine
{
    public class HexEngine
    {
        private readonly Board _board;
        private CurrentColor _currentColor;//For the starting player

        private List<GridPosition> _validPositions = new List<GridPosition>();

        public HexEngine(Board board, CurrentColor currentColor)
        {
            _board = board;
            _currentColor = currentColor;

            InitializeBoard();
            board.PositionClicked += Board_PositionClicked;
            board.TileHoverExited += Board_TileHoverExited;

            board.CurrentPlayersColor = _currentColor;
        }

        private void Board_TileHoverExited(object sender, EventArgs e)
        {
            _board.HighlightTiles(_validPositions);
        }

        private void Board_PositionClicked(object sender, PositionEventArgs e)
        {
            ChangeCurrentColor();
        }

        private void ChangeCurrentColor()
        {

            if (_currentColor == CurrentColor.Black)
            {
                _currentColor = CurrentColor.White;
            }
            else
            {
                _currentColor = CurrentColor.Black;
            }

            foreach (Tile tile in _board.Tiles.Values)
            {
                tile.IsHighlighted = false;
                tile.PiecesToBeFlipped.Clear();

                tile.IsHoverHighlighted = false;
                tile.TilesToBeHighlightedWhenHoveringOver.Clear();
            }

            _board.CurrentPlayersColor = _currentColor;

            FindValidTiles();
        }

        public void FindValidTiles()
        {
            _validPositions.Clear();

            BaseMoveSet moveSet = new BaseMoveSet();
            
            foreach (Piece piece in _board.Pieces.Values)
            {
                if (piece.CurrentColor == _currentColor)
                {
                    _validPositions.AddRange(moveSet.GetMovePositions(_board, piece));
                }
            }

            if (_validPositions != null)
            {
                _board.HighlightTiles(_validPositions);
            }
            
        }

        private void InitializeBoard()
        {
            _board.SpawnPiece(new GridPosition(0, 0, 0), CurrentColor.Black);

            _board.SpawnPiece(new GridPosition(0, -1, 1), CurrentColor.White);//
            _board.SpawnPiece(new GridPosition(0, 1, -1), CurrentColor.Black);

            _board.SpawnPiece(new GridPosition(1, 0, -1), CurrentColor.White);//
            _board.SpawnPiece(new GridPosition(-1, 0, 1), CurrentColor.Black);

            _board.SpawnPiece(new GridPosition(1, -1, 0), CurrentColor.Black);
            _board.SpawnPiece(new GridPosition(-1, 1, 0), CurrentColor.White);//

            //
            //_board.SpawnPiece(new GridPosition(0, 0, 0), CurrentColor.White);

            //_board.SpawnPiece(new GridPosition(0, -1, 1), CurrentColor.Black);
            //_board.SpawnPiece(new GridPosition(0, 1, -1), CurrentColor.Black);

            //_board.SpawnPiece(new GridPosition(1, 0, -1), CurrentColor.Black);
            //_board.SpawnPiece(new GridPosition(-1, 0, 1), CurrentColor.Black);

            //_board.SpawnPiece(new GridPosition(1, -1, 0), CurrentColor.Black);
            //_board.SpawnPiece(new GridPosition(-1, 1, 0), CurrentColor.Black);
            //_board.SpawnPiece(new GridPosition(-2, 2, 0), CurrentColor.Black);
        }
    }
}
