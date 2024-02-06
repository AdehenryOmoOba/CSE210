using System;
using System.Collections.Generic;

namespace shapes
{
    class Circle : Shape
    {
        private double _radius;

        public Circle(string color, double radius)
        {
            SetColor(color);
            _radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
    }
}