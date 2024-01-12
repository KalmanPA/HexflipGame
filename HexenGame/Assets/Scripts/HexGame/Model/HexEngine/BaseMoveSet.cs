using System;
using System.Collections;
using System.Collections.Generic;

namespace Hex.Model.HexEngine
{
    public class BaseMoveSet
    {
        private readonly int _maxTileDistance = 6;

        private readonly GridPosition _invalidPosition = new GridPosition(5,5,5);

        public List<GridPosition> GetMovePositions(Board board, Piece piece)
        {

            List<GridPosition> validPositions = new List<GridPosition>();

            validPositions.Add(CheckIfPositionIsAvalible(board, piece, -1, 1, 0));
            validPositions.Add(CheckIfPositionIsAvalible(board, piece, 1, -1, 0));

            validPositions.Add(CheckIfPositionIsAvalible(board, piece, -1, 0, 1));
            validPositions.Add(CheckIfPositionIsAvalible(board, piece, 1, 0, -1));

            validPositions.Add(CheckIfPositionIsAvalible(board, piece, 0, 1, -1));
            validPositions.Add(CheckIfPositionIsAvalible(board, piece, 0, -1, 1));

            //foreach(Hexdirection direction in Enum.Getvalues(tyof(Hexdirection)))

            return validPositions;
        }

        private GridPosition CheckIfPositionIsAvalible(Board board, Piece piece, int q, int r, int s)
        {
            GridPosition position = new GridPosition();
            bool isThereOppositePiece = false;

            List<Piece> piecePositionsThatAreSupposedToBeFlipped = new List<Piece>();

            List<Tile> tilesToBeHiglightedByHoveringOnValidTile = new List<Tile>();

            for (int i = 1; i < _maxTileDistance; i++)
            {

                GridPosition pos = piece.GridPosition;

                GridPosition checkPosition = pos + new GridPosition(q,r,s)*i;

                Piece otherPiece = board.GetPieceOnPosition(checkPosition);

                tilesToBeHiglightedByHoveringOnValidTile.Add(board.GetTileOnPosition(pos));

                if (otherPiece == null && !isThereOppositePiece)
                {
                    break;
                }

                if (otherPiece == null)
                {

                    tilesToBeHiglightedByHoveringOnValidTile.Add(board.GetTileOnPosition(checkPosition));
                    position = checkPosition;
                    break;
                }

                if (otherPiece.CurrentColor == piece.CurrentColor)
                {
                    break;
                }

                tilesToBeHiglightedByHoveringOnValidTile.Add(board.GetTileOnPosition(otherPiece.GridPosition));
                piecePositionsThatAreSupposedToBeFlipped.Add(otherPiece);
                isThereOppositePiece = true;

                //if (otherPiece.CurrentColor != piece.CurrentColor)
                //{
                //    piecePositionsThatAreSupposedToBeFlipped.Add(otherPiece);
                //    isThereOppositePiace = true;
                //}
            }

            if (position != _invalidPosition)
            {
                Tile tile = board.GetTileOnPosition(position);

                if (tile != null)
                {
                    tile.PiecesToBeFlipped.AddRange(piecePositionsThatAreSupposedToBeFlipped);
                    tile.TilesToBeHighlightedWhenHoveringOver.AddRange(tilesToBeHiglightedByHoveringOnValidTile);
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
