using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

namespace Hex.Model
{
    public enum CurrentColor
    {
        White,
        Black
    }
    public class Piece
    {
        //public CurrentColor CurrentColor => _currentColor;

        public event EventHandler Flipped;

        public event EventHandler Removed;

        public GridPosition GridPosition { get; set; }

        public CurrentColor CurrentColor
        {
            get => _currentColor; set
            {
                if (_currentColor == value) return;
                _currentColor = value;
                Flipped?.Invoke(this, EventArgs.Empty);
                //if (_currentColor == CurrentColor.Black)
                //{
                //    _currentColor = CurrentColor.White;
                //}
                //else
                //{
                //    _currentColor = CurrentColor.Black;
                //}
            }
        }
        //private bool _isFlipped = false;
        private readonly Board _board;
        //private readonly GridPosition _position;
        private CurrentColor _currentColor;

        public Piece(Board board, GridPosition position, CurrentColor currentColor)
        {
            _board = board;
            GridPosition = position;
            _currentColor = currentColor;

            //if (currentColor == CurrentColor.Black)
            //{
            //    Flipped?.Invoke(this, EventArgs.Empty);
            //}
        }

        public void Remove()
        {
            Removed?.Invoke(this, EventArgs.Empty);
        }
    }
}
