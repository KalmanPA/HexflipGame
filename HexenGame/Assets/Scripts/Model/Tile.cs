using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UIElements;

namespace Hex.Model
{
    public class Tile
    {
        public List<Piece> PiecesToBeFlipped = new List<Piece>();
        //Functionalities: regidter clicks, change color

        public event EventHandler Clicked;
        public event EventHandler IsHighlightChanged; //We create this event,
                                                      //so that the Tile can inform the TileView abot the highlight change

        public GridPosition GridPosition { get; private set; }

        //So, we detect the click in the TileView which invokes the Click event in the Tile,
        //which runs a subscribed method in the Board that changes the Tile's Higlight parameter,
        //which invokes the highlight changed event and that runs a subscribet method in the TileView that changes the material.

        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                if (_isHighlighted != value)
                {
                    _isHighlighted = value;
                    //This is where we trigger a function that triggers the event
                    OnIsHighlightChanged();
                }
                _isHighlighted = value;
            }
        }
        private bool _isHighlighted;

        public Tile(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
        }

        public void Click()
        {
            //Gtes called in TileView
            OnClick();
        }

        private void OnClick()
        {   
            foreach (Piece piece in PiecesToBeFlipped)
            {
                if (piece.CurrentColor == CurrentColor.White)
                {
                    piece.CurrentColor = CurrentColor.Black;
                }
                else
                {
                    piece.CurrentColor = CurrentColor.White;
                }
            }

            Clicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnIsHighlightChanged()
        {
            //We invoke the event when the highlight gets changed
            IsHighlightChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
