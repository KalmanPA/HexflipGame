using System.Collections;
using System.Collections.Generic;
using System;


namespace Hex.Model
{
    public class PositionEventArgs : EventArgs
    {
        public GridPosition GridPosition { get; private set; }
        public PositionEventArgs(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
        }
    }
}
