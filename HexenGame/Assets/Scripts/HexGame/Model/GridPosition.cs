using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using UnityEngine;

namespace Hex.Model
{
    public struct FracHexCoordinate
    {
        public float Q { get; set; }
        public float R { get; set; }
        public float S { get; set; }

        public FracHexCoordinate(float q, float r, float s)
        {
            Q = q;
            R = r;
            S = s;
        }
        public FracHexCoordinate(float q, float r) : this(q, r, -q - r) { }
        public readonly GridPosition CubeRound()
        {
            int q = Convert.ToInt32(Q);
            int r = Convert.ToInt32(R);
            int s = Convert.ToInt32(S);

            float qDiff = MathF.Abs(q - Q);
            float rDiff = MathF.Abs(r - R);
            float sDiff = MathF.Abs(s - S);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                q = -r - s;
            }
            else if (rDiff > sDiff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }
            return new GridPosition(q, r, s);
        }
        public static GridPosition CubeRound(FracHexCoordinate frac) => frac.CubeRound();
    }
    public struct GridPosition
    {
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }

        #region Directions
        public enum Direction
        {
            TopLeft,
            TopRight,
            Right,
            BottomRight,
            BottomLeft,
            Left,
            DirectionsCount
        }
        private static readonly GridPosition[] _directions =
            {new GridPosition(0,-1,1),
         new GridPosition(1,-1,0),
         new GridPosition(1,0,-1),
         new GridPosition(0,1,-1),
         new GridPosition(-1,1,0),
         new GridPosition(-1,0,1)};

        public enum DiagonalDirection
        {
            Top,
            TopRight,
            BottomRight,
            Bottom,
            BottomLeft
        }
        private static readonly GridPosition[] _diagonalDirections =
            {new GridPosition(1,-2,1),
         new GridPosition(2,-1,-1),
         new GridPosition(1,1,-2),
         new GridPosition(-1,2,-1),
         new GridPosition(-2,1,1),
         new GridPosition(-1,-1,2)};

        public static GridPosition GetDirection(Direction direction) => _directions[(int)direction];
        public static GridPosition CubeDiagonalDirection(DiagonalDirection direction) => _diagonalDirections[(int)direction];
        public static GridPosition Zero => new(0, 0, 0);
        public readonly GridPosition GetNeighbor(Direction direction) => this + GetDirection(direction);
        public readonly GridPosition GetDiagonalNeighbor(DiagonalDirection direction) => this + CubeDiagonalDirection(direction);

        #endregion
        public GridPosition(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;

            Debug.Assert(Q + R + S == 0);
        }
        public GridPosition(int q, int r) : this(q, r, -q - r) { }


        public static int CubeDistance(GridPosition a, GridPosition b)
        {
            var vec = a - b;
            return (Math.Abs(vec.Q) + Math.Abs(vec.R) + Math.Abs(vec.S)) / 2;
        }
        public readonly int CubeDistance(GridPosition other) => CubeDistance(this, other);

        #region Operators Overides
        public override readonly string ToString() => $"Cubic Coordinate: ({Q},{R},{S})";
        public override readonly bool Equals(object obj)
        {
            return obj is GridPosition coordinate &&
                   Q == coordinate.Q &&
                   R == coordinate.R &&
                   S == coordinate.S;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Q, R, S);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b) => new(a.Q + b.Q, a.R + b.R, a.S + b.S);
        public static GridPosition operator +(GridPosition a) => a;
        public static GridPosition operator -(GridPosition a, GridPosition b) => a + (-b);
        public static GridPosition operator -(GridPosition a) => new(-a.Q, -a.R, -a.S);
        public static GridPosition operator *(GridPosition a, int s) => new(s * a.Q, s * a.R, s * a.S);
        public static GridPosition operator *(int s, GridPosition a) => a * s;
        public static bool operator ==(GridPosition a, GridPosition b) => a.Equals(b);
        public static bool operator !=(GridPosition a, GridPosition b) => !a.Equals(b);
        #endregion

    }
    //public struct GridPosition
    //{
    //    //This represents the rows and columns of the board.
    //    //We made this because we do not want to use Unity's vector types

    //    public int R { get; set; }
    //    public int Q { get; set; }
    //    public int S { get; set; }

    //    public GridPosition(int r, int q, int s)
    //    {
    //        R = r;
    //        Q = q;
    //        S = s;
    //    }
    //    public override string ToString()
    //    {
    //        return $"R: {R} Q: {Q} S: {S}";
    //    }


    //}
}

