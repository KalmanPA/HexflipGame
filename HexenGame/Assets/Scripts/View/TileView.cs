using Hex.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Hex.View
{

    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        //We add the interface that is built in to detect clicks
        public Tile TileModel { get; private set; }//The tile object for the TileView

        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _highlightedMaterial;

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
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //TileModel.IsHighlighted = true;
            //The function of the built in intrface
            Debug.Log($"Tile{gameObject} is on {TileModel.GridPosition}");

            if (TileModel.IsHighlighted)
            {
                TileModel.Click();//After dtecting a click on the object it calles the cliced method of the Tile,
                                  //which will call the clicked event
            }
        }

        private void Tile_IsHighlightChanged(object sender, EventArgs e)
        {
            //The method that changes the color and is subscribed to the Tiles Higlight changing event
            _renderer.sharedMaterial = TileModel.IsHighlighted ? _highlightedMaterial : _defaultMaterial;
        }
    }
}
