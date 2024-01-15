using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Hex.View
{

    public class TileView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //We add the interface that is built in to detect clicks
        public Tile TileModel { get; private set; }//The tile object for the TileView

        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _highlightedMaterial;

        [SerializeField] private Material _hoverHighlightedMaterial;

        [SerializeField] private Material _hoverFlippHighlightedMaterial;

        private Material _defaultMaterial;

        private void Awake()
        {
            //Getting and setting the material
            Assert.IsNotNull(_renderer);
            Assert.IsNotNull(_highlightedMaterial);

            _defaultMaterial = _renderer.sharedMaterial;
        }

        public void SetModel(Tile tile)//Gets called in BoardView
        {
            TileModel = tile;//Sets the TileModel to the reciaved tile
            tile.IsHighlightChanged += Tile_IsHighlightChanged;//Subscribes the color changing method
                                                               //to the tiles higlightchanged event

            tile.IsHoverHighlightChanged += Tile_IsHoverHighlightChanged;

            tile.IsHoverFlippHighlightChanged += Tile_IsHoverFlippHighlightChanged; 
        }

        private void Tile_IsHoverFlippHighlightChanged(object sender, EventArgs e)
        {
            _renderer.sharedMaterial = TileModel.IsHoverFlippHighlighted ? _hoverFlippHighlightedMaterial : _defaultMaterial;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //TileModel.IsHighlighted = true;
            //The function of the built in intrface
            //Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");

            if (TileModel.IsHoverFlippHighlighted)
            {
                //TileModel.IsHoverFlippHighlighted = false;

                TileModel.Click();

                return;
            }

            if (TileModel.IsHighlighted || TileModel.IsHoverHighlighted)//We only want to run this if the tile is among the higlighted ones
            {
                TileModel.Click();//After dtecting a click on the object it calles the cliced method of the Tile,
                                  //which will call the clicked event
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");



            if (TileModel.IsHighlighted)//We only want to run this if the tile is among the higlighted ones
            {
                //TileModel.IsHoverHighlighted = true;
                TileModel.OnHighlightedHoverEnter();
                //Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");

                return;
            }

            TileModel.OnHoverEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TileModel.IsHoverFlippHighlighted = false;

            if (TileModel.IsHighlighted || TileModel.IsHoverHighlighted)//We only want to run this if the tile is among the higlighted ones
            {
                //Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");
                TileModel.OnHoverExit();
                
            }
        }

        private void Tile_IsHoverHighlightChanged(object sender, EventArgs e)
        {
            //Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");
            _renderer.sharedMaterial = TileModel.IsHoverHighlighted ? _hoverHighlightedMaterial : _defaultMaterial;
            
        }

        private void Tile_IsHighlightChanged(object sender, EventArgs e)
        {
            //The method that changes the color and is subscribed to the Tiles Higlight changing event
            _renderer.sharedMaterial = TileModel.IsHighlighted ? _highlightedMaterial : _defaultMaterial;
        }

        
    }
}
