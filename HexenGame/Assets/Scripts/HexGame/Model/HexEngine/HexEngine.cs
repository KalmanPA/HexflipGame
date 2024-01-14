using CommandPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hex.Model.HexEngine
{
    public class HexEngine
    {
        public string WinnerTeam;

        public int NumberOfWhitePieces;
        public int NumberOfBlackPieces;

        public event EventHandler GameOver;

        public event EventHandler PlayerColorChanged;

        //private readonly GridPosition _invalidPosition = new GridPosition(5,5,5);

        private readonly Board _board;
        public CurrentColor CurrentColor;//For the starting player

        private List<GridPosition> _validPositions = new List<GridPosition>();

        public HexEngine(Board board, CurrentColor currentColor)
        {
            _board = board;
            CurrentColor = currentColor;

            InitializeBoard();
            board.PositionClicked += Board_PositionClicked;
            board.TileHoverExited += Board_TileHoverExited;

            board.CurrentPlayersColor = CurrentColor;
        }

        public void Undo()
        {
            _board.Undo();
        }
        public void Redo()
        {
            _board.Redo();
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

            if (CurrentColor == CurrentColor.Black)
            {
                CurrentColor = CurrentColor.White;
            }
            else
            {
                CurrentColor = CurrentColor.Black;
            }

            foreach (Tile tile in _board.Tiles.Values)
            {
                tile.IsHighlighted = false;
                tile.PiecesToBeFlipped.Clear();

                tile.IsHoverHighlighted = false;
                tile.TilesToBeHighlightedWhenHoveringOver.Clear();
            }

            _board.CurrentPlayersColor = CurrentColor;

            CountNumberOfPieces();

            PlayerColorChanged?.Invoke(this, EventArgs.Empty);

            FindValidTiles();
        }

        public void CountNumberOfPieces()
        {
            NumberOfBlackPieces = 0;
            NumberOfWhitePieces = 0;

            foreach (Piece piece in _board.Pieces.Values)
            {
                if (piece.CurrentColor == CurrentColor.White)
                {
                    NumberOfWhitePieces++;
                }
                else
                {
                    NumberOfBlackPieces++;
                }
            }
        }

        public void FindValidTiles()
        {
            _validPositions.Clear();

            BaseMoveSet moveSet = new BaseMoveSet();
            
            foreach (Piece piece in _board.Pieces.Values)
            {
                if (piece.CurrentColor == CurrentColor)
                {
                    _validPositions.AddRange(moveSet.GetMovePositions(_board, piece));
                }
            }

            if (_validPositions != null)
            {
                _board.HighlightTiles(_validPositions);
            }



            //foreach (GridPosition positin in _validPositions)
            //{
            //    if (positin == new GridPosition(5,5,5))
            //    {
            //        _validPositions.Remove(positin);
            //    }
            //}

            //RemoveInvalidPositions(_validPositions, _invalidPosition);


            if (AreTilesHighlighted())
            {
                WinnerTeam = DecideWinner();

                GameOver?.Invoke(this, EventArgs.Empty);
            }


        }

        private string DecideWinner()
        {
            if (NumberOfWhitePieces > NumberOfBlackPieces)
            {
                return "White";
            }
            else if (NumberOfWhitePieces < NumberOfBlackPieces)
            {
                return "Black";
            }
            else
            {
                return "Draw";
            }
        }

        private bool AreTilesHighlighted()
        {
            foreach (Tile tile in _board.Tiles.Values)
            {
                if (tile.IsHighlighted)
                {
                    // If any element is true, return false
                    return false;
                }
            }
            // If no true element is found, return true
            return true;
        }

        //private void RemoveInvalidPositions(List<GridPosition> list, GridPosition vectorToRemove)
        //{
        //    // Removes invalid positions
        //    list.RemoveAll(item => item == vectorToRemove);
        //}

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
