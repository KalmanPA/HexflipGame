using CommandPattern;
using Hex.Model.HexEngine.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEngine;

namespace Hex.Model
{
    public class Board
    {
        public event EventHandler<PieceEventArgs> PieceSpawned;
        public event EventHandler<PositionEventArgs> PositionClicked;

        public event EventHandler TileHoverExited;

        public CurrentColor CurrentPlayersColor;

        private CommandInvoker _commandInvoker;

        //Creates a new Dictionary with the board's tiles and their corresponding grid positions
        public Dictionary<GridPosition, Tile> Tiles = new Dictionary<GridPosition, Tile>();

        public Dictionary<GridPosition, Piece> Pieces = new Dictionary<GridPosition, Piece>();

        private bool _isWhiteAbleToFlip = true;
        private bool _isBlackAbleToFlip = true;

        public Board()
        {
            _commandInvoker = new CommandInvoker();
        }

        public void Undo()
        {
            _commandInvoker.Undo();
        }
        public void Redo()
        {
            _commandInvoker.Redo();
        }

        public Tile AddTile(GridPosition position)
        {
            //A method that creates a new tile, adds it to the Dictionary and returns it. Gets called in BoardView.
            Tile tile = new Tile(position);
            tile.Clicked += Tile_Clicked;//Subscribe method that will change their Highlight property
            tile.HighlightHoverEntered += Tile_HighlightHoverEntered;
            tile.HoverExited += Tile_HoverExited;

            tile.HoverEntered += Tile_HoverEntered;

            Tiles.Add(position, tile);//Adds it to Dictionary

            return tile;//Need it for the BoardView to assign it to the TileView
        }

        private void Tile_HoverEntered(object sender, EventArgs e)
        {
            if (sender is Tile tileModel)
            {
                if (GetPieceOnPosition(tileModel.GridPosition) != null)
                {
                    if (_isWhiteAbleToFlip && CurrentPlayersColor == CurrentColor.White)
                    {

                        tileModel.IsHoverFlippHighlighted = true;
                    }
                    else if(_isBlackAbleToFlip && CurrentPlayersColor == CurrentColor.Black)
                    {
                        
                        tileModel.IsHoverFlippHighlighted = true;
                    }
                }
            }
        }

        private void Tile_HoverExited(object sender, EventArgs e)
        {
            

            if (sender is Tile tileModel)
            {
                //tileModel.IsHoverFlippHighlighted = false;

                foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
                {
                    tile.IsHoverHighlighted = false;
                    
                }

                TileHoverExited?.Invoke(this, EventArgs.Empty);
            }


        }

        private void Tile_HighlightHoverEntered(object sender, EventArgs e)
        {

            if (sender is Tile tileModel)
            {
                foreach (Tile tile in Tiles.Values)
                {
                    tile.IsHighlighted = false;
                }

                foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
                {
                    tile.IsHoverHighlighted = true;
                }
            }
        }

        private void Tile_Clicked(object sender, System.EventArgs e)
        {
            ////TEMP: highlight the tile
            //if (sender is Tile tileModel)//We need to check if a Tile is sending it to access its parameter
            //{
            //    tileModel.IsHighlighted = true;//We change the tiles Highlight here
            //}

            if (sender is Tile tileModel)
            {
                if (tileModel.IsHoverFlippHighlighted)
                {
                    //FlippPiece(tileModel);
                    _commandInvoker.ExecuteCommand(new FlippSinglePiece(this, tileModel));

                    return;
                }

                _commandInvoker.ExecuteCommand( new PlacePieceCommand(this, tileModel.GridPosition, CurrentPlayersColor) );

                //SpawnPiece(tileModel.GridPosition, CurrentPlayersColor);

                //PositionClicked?.Invoke(this, new PositionEventArgs(tileModel.GridPosition));

                //foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
                //{
                //    tile.IsHoverHighlighted = false;
                //}



            }


        }

        public void FlippPiece(Tile tileModel)
        {
            Piece piece = GetPieceOnPosition(tileModel.GridPosition);

            tileModel.IsHoverFlippHighlighted = false;

            if (piece.CurrentColor == CurrentColor.White)
            {
                piece.CurrentColor = CurrentColor.Black;
                //_isBlackAbleToFlip = false;
            }
            else
            {
                piece.CurrentColor = CurrentColor.White;
                //_isWhiteAbleToFlip = false;
            }

            if (CurrentPlayersColor == CurrentColor.White)
            {
                _isWhiteAbleToFlip = false;
            }
            else
            {
                _isBlackAbleToFlip = false;
            }



            PositionClicked?.Invoke(this, new PositionEventArgs(tileModel.GridPosition));
        }

        public void FlippBackPiece(Tile tileModel)
        {
            Piece piece = GetPieceOnPosition(tileModel.GridPosition);

            //tileModel.IsHoverFlippHighlighted = true;

            if (piece.CurrentColor == CurrentColor.White)
            {
                piece.CurrentColor = CurrentColor.Black;
                //_isBlackAbleToFlip = true;
            }
            else
            {
                piece.CurrentColor = CurrentColor.White;
                //_isWhiteAbleToFlip = true;
            }

            if (CurrentPlayersColor == CurrentColor.White)
            {
                _isBlackAbleToFlip = true;
            }
            else
            {
                _isWhiteAbleToFlip = true;
            }



            PositionClicked?.Invoke(this, new PositionEventArgs(tileModel.GridPosition));
        }

        public Tile GetTileOnPosition(GridPosition position)
        {
            return Tiles.GetValueOrDefault(position);//Gives us the Tile at a certain GridPosition from the Dictionary
        }

        public Piece GetPieceOnPosition(GridPosition position)
        {
            return Pieces.GetValueOrDefault(position);
        }

        public Piece SpawnPiece(GridPosition pos, CurrentColor color)
        {
            Tile tileModel = Tiles.GetValueOrDefault(pos);

            foreach (Piece toBeFlippedPiece in tileModel.PiecesToBeFlipped)
            {
                if (toBeFlippedPiece.CurrentColor == CurrentColor.White)
                {
                    toBeFlippedPiece.CurrentColor = CurrentColor.Black;
                }
                else
                {
                    toBeFlippedPiece.CurrentColor = CurrentColor.White;
                }
            }

            tileModel.PiecesToBeReflipped.AddRange(tileModel.PiecesToBeFlipped);


            Piece piece = new Piece(this, pos, color);

            Pieces.Add(pos, piece);

            PieceSpawned?.Invoke(this, new PieceEventArgs(piece));


            PositionClicked?.Invoke(this, new PositionEventArgs(tileModel.GridPosition));

            foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
            {
                tile.IsHoverHighlighted = false;
            }


            return piece;
        }

        public void RemovePiece(GridPosition pos)
        {
            if (Pieces.ContainsKey(pos))
            {
                Pieces[pos].Remove();
                Pieces.Remove(pos);
            }

            Tile tile = GetTileOnPosition(pos);

            foreach (Piece piece in tile.PiecesToBeReflipped)
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

            tile.PiecesToBeReflipped.Clear();

            PositionClicked?.Invoke(this, new PositionEventArgs(pos));
        }

        //This was not in the PDF. Could also have been in the Chess Engine if done a bit differently
        public void HighlightTiles(List<GridPosition> positions)
        {
            //foreach (Tile tile in _tiles.Values)
            //{
            //    tile.IsHighlighted = false;
            //}

            foreach (GridPosition position in positions)
            {
                Tile tile = GetTileOnPosition(position);
                if (tile == null) continue;

                tile.IsHighlighted = true;
            }
        }
    }

}
