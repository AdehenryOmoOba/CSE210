using System;
using System.Collections.Generic;

namespace shapes
{
    class Shape
    {
        protected string _color;

        public void SetColor(string color)
        {
            _color = color;
        }

        public string GetColor()
        {
            return _color;
        }

        public virtual double GetArea()
        {
            return 0.0; // Default implementation for the base class
        }
    }
}