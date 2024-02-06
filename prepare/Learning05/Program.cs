using System;
using System.Collections.Generic;




namespace shapes
{
    class Program
    {
        static void Main()
        {
            List<Shape> shapes = new List<Shape>();

            shapes.Add(new Square("Red", 5.0));
            shapes.Add(new Rectangle("Blue", 4.0, 6.0));
            shapes.Add(new Circle("Green", 3.0));

            foreach (var shape in shapes)
            {
                Console.WriteLine($"Shape: {shape.GetType().Name}, Color: {shape.GetColor()}, Area: {shape.GetArea()}");
            }
        }
    }
}

