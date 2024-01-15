using System;
using System.Collections;
using System.Collections.Generic;

namespace Hex.Model
{
    public class Player
    {

        public float Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    
                    CalculateProportionalScore();
                }
                _score = value;
            }
        }
        private float _score;

        public float ProportionalScore;
        

        public float NumberOfPieces;

        private void CalculateProportionalScore()
        {
            ProportionalScore = _score / NumberOfPieces;


        }

        

        
    }

}
