using System;
using System.Collections;
using System.Collections.Generic;


namespace Hex.Model
{
    public class FloatEventArguments : EventArgs
    {
        public float Number { get; }
        public FloatEventArguments(float number)
        {
            Number = number;
        }
    }
}
