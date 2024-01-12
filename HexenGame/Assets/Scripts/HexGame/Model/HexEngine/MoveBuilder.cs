using System.Collections;
using System.Collections.Generic;

namespace Hex.Model.HexEngine
{
    public class MoveBuilder
    {
        const int MAX_LINEAR_STEPS = 7;
        private readonly Board _board;
        private readonly Piece _piece;
        private List<GridPosition> _positions;
        public MoveBuilder(Board board, Piece piece)
        {
            _board = board;
            _piece = piece;
            _positions = new List<GridPosition>();
        }

        public MoveBuilder AddMove(int offsetX, int offsetY, int maxSteps = MAX_LINEAR_STEPS)
        {
            if (_piece.CurrentColor == CurrentColor.Black)
            {
                offsetX *= -1;
                offsetY *= -1;
            }

            GridPosition nextPos = _piece.GridPosition;
            for (int stepCount = 1; stepCount <= maxSteps; ++stepCount)
            {
                nextPos.Q += offsetX;
                nextPos.R += offsetY;

                //Invalid position
                if (_board.GetTileOnPosition(nextPos) == null) return this;

                Piece otherpiece = _board.GetPieceOnPosition(nextPos);
                //If tile is empty, or we can capture, add position
                if (otherpiece == null)
                {
                    _positions.Add(nextPos);
                }
                else
                {
                    if (otherpiece.CurrentColor != _piece.CurrentColor)
                        _positions.Add(nextPos);
                    return this;
                }

            }
            return this;
        }

        public List<GridPosition> GetPositions()
        {
            return _positions;
        }
    }
}
