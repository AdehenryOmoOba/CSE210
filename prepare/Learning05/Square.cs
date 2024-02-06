using System;
using System.Collections.Generic;

namespace shapes
{
    class Square : Shape
    {
        private double _side;

        public Square(string color, double side)
        {
            SetColor(color);
            _side = side;
        }

        public override double GetArea()
        {
            return _side * _side;
        }
    }
}