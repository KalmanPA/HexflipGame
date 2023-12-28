using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

namespace Hex.Model
{
    public class Board
    {
        public event EventHandler<PieceEventArgs> PieceSpawned;
        public event EventHandler<PositionEventArgs> PositionClicked;

        public event EventHandler TileHoverExited;

        public CurrentColor CurrentPlayersColor;

        //Creates a new Dictionary with the board's tiles and their corresponding grid positions
        public Dictionary<GridPosition, Tile> Tiles = new Dictionary<GridPosition, Tile>();

        public Dictionary<GridPosition, Piece> Pieces = new Dictionary<GridPosition, Piece>();

        public Tile AddTile(GridPosition position)
        {
            //A method that creates a new tile, adds it to the Dictionary and returns it. Gets called in BoardView.
            Tile tile = new Tile(position);
            tile.Clicked += Tile_Clicked;//Subscribe method that will change their Highlight property
            tile.HoverEntered += Tile_HoverEntered;
            tile.HoverExited += Tile_HoverExited;

            Tiles.Add(position, tile);//Adds it to Dictionary

            return tile;//Need it for the BoardView to assign it to the TileView
        }

        private void Tile_HoverExited(object sender, EventArgs e)
        {
            if (sender is Tile tileModel)
            {
                foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
                {
                    tile.IsHoverHighlighted = false;
                }

                TileHoverExited?.Invoke(this, EventArgs.Empty);
            }


        }

        private void Tile_HoverEntered(object sender, EventArgs e)
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
                SpawnPiece(tileModel.GridPosition, CurrentPlayersColor);

                PositionClicked?.Invoke(this, new PositionEventArgs(tileModel.GridPosition));

                foreach (Tile tile in tileModel.TilesToBeHighlightedWhenHoveringOver)
                {
                    tile.IsHoverHighlighted = false;
                }

                

            }


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
            Piece piece = new Piece(this, pos, color);

            Pieces.Add(pos, piece);

            PieceSpawned?.Invoke(this, new PieceEventArgs(piece));

            return piece;
        }

        public void RemovePiece(GridPosition pos)
        {
            if (Pieces.ContainsKey(pos))
            {
                Pieces[pos].Remove();
                Pieces.Remove(pos);
            }
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
