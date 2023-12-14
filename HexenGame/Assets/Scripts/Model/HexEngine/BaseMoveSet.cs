using System;
using System.Collections;
using System.Collections.Generic;

namespace Hex.Model.HexEngine
{
    public class BaseMoveSet
    {
        int _maxTileDistance = 6;

        private readonly GridPosition _invalidPosition = new GridPosition(5,5,5);
        public List<GridPosition> GetMovePositions(Board board, Piece piece)
        {

            List<GridPosition> positions = new List<GridPosition>();

            //GridPosition positionToBeAdded;
       

            positions.Add(CheckIfPositionIsAvalible(board, piece, -1, 1, 0));
            positions.Add(CheckIfPositionIsAvalible(board, piece, 1, -1, 0));

            positions.Add(CheckIfPositionIsAvalible(board, piece, -1, 0, 1));
            positions.Add(CheckIfPositionIsAvalible(board, piece, 1, 0, -1));

            positions.Add(CheckIfPositionIsAvalible(board, piece, 0, 1, -1));
            positions.Add(CheckIfPositionIsAvalible(board, piece, 0, -1, 1));

            //positions.Add(new GridPosition(66, 346, 334));

            return positions;
        }

        private GridPosition CheckIfPositionIsAvalible(Board board, Piece piece, int q, int r, int s)
        {
            GridPosition position = new GridPosition();
            bool isThereOppositePiace = false;

            List<Piece> piecePositionsThatAreSupposedToBeFlipped = new List<Piece>();

            for (int i = 1; i < _maxTileDistance; i++)
            {

                GridPosition pos = piece.GridPosition;

                GridPosition checkPosition = pos + new GridPosition(q,r,s)*i;

                Piece otherPiece = board.GetPieceOnPosition(checkPosition);

                if (otherPiece == null && !isThereOppositePiace)
                {
                    position = _invalidPosition;
                    break;
                }

                if (otherPiece == null)
                {
                    position = checkPosition;
                    break;
                }

                if (otherPiece.CurrentColor == piece.CurrentColor)
                {
                    position = _invalidPosition;
                    break;
                }

                if (otherPiece.CurrentColor != piece.CurrentColor)
                {
                    piecePositionsThatAreSupposedToBeFlipped.Add(otherPiece);
                    isThereOppositePiace = true;
                }
            }

            if (position != _invalidPosition)
            {
                Tile tile = board.GetTileOnPosition(position);

                if (tile != null)
                {
                    tile.PiecesToBeFlipped.AddRange(piecePositionsThatAreSupposedToBeFlipped);
                }

                
            }

            if (position == new GridPosition(0,0,0))
            {
                position = _invalidPosition;
            }
            return position;
        }
    }
}
