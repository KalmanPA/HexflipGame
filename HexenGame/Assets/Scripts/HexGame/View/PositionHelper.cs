using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View
{
    public static class PositionHelper
    {
        public static GridPosition WorldToGrid(this Vector3 position)
        {
            float q = Mathf.Sqrt(3) / 3 * position.x - 1f / 3 * position.z;
            float r = 2f / 3 * position.z;
            FracHexCoordinate frac = new(q, r);
            return frac.CubeRound();
        }
        public static Vector3 GridToWorld(this GridPosition cubicCoordinate)
        {
            float x = Mathf.Sqrt(3) * cubicCoordinate.Q + Mathf.Sqrt(3) / 2 * cubicCoordinate.R;
            float z = 3f / 2f * cubicCoordinate.R;
            return new Vector3(x, 0, z);
        }
        //Converts our custom Position struct from and to a Vector3 world position.
        //Since this class will be necessary to determine where the actual game objects should be placed
        //based on their grid position.


        //private static readonly int size = 1;

        //public static GridPosition WorldToGrid(Vector3 position)
        //{
        //    float q = 1 / size * (Mathf.Sqrt(3f) / 3 * position.x + 1f / 3 * position.z);
        //    float r = 1 / size * (-2f / 3 * position.z);
        //    return FloatCubetoIntCube(q, r);
        //}

        //public static Vector3 GridToWorld(GridPosition coordinate)
        //{
        //    float x = size * (Mathf.Sqrt(3f) * coordinate.Q + Mathf.Sqrt(3f) / 2 * coordinate.R);
        //    float z = size * (3f / 2 * coordinate.R);
        //    return new Vector3(x, 0, z);
        //}

        //private static GridPosition FloatCubetoIntCube(float floatQ, float floatR)
        //{
        //    float floatS = -floatQ - floatR;

        //    int intQ = Mathf.RoundToInt(floatQ);
        //    int intR = Mathf.RoundToInt(floatR);
        //    int intS = Mathf.RoundToInt(floatS);

        //    float qDiff = Mathf.Abs(intQ - floatQ);
        //    float rDiff = Mathf.Abs(intR - floatR);
        //    float sDiff = Mathf.Abs(intS - floatS);

        //    if (qDiff > rDiff && qDiff > sDiff) intQ = -intR - intS;
        //    else if (rDiff > sDiff) intR = -intQ - intS;
        //    else intS = -intQ - intR;

        //    return new GridPosition(intR, intQ, intS);
        //}


    }
}
