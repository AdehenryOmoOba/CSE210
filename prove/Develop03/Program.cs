using System;

class Program
{
    static void Main(string[] args)
    {

        Fraction fraction = new Fraction();

        fraction.SetNumerator(3);
        fraction.SetDenominator(4);

        int numerator = fraction.GetNumerator();
        int denominator = fraction.GetDenominator();

        Console.WriteLine($"Numerator: {numerator}");   // Output: 3
        Console.WriteLine($"Denominator: {denominator}");     // Output: 4

        string fractionString = fraction.GetFractionString();
        double decimalValue = fraction.GetDecimalValue();

        // Print results
        Console.WriteLine($"Result in fraction: {fractionString}");   // Output: 3/4
        Console.WriteLine($"Result in decimal: {decimalValue}");     // Output: 0.75

    }


    public class Fraction
    {
        private int _numerator;
        private int _denominator;

        // Constructor with no parameters
        public Fraction()
        {
            _numerator = 1;
            _denominator = 1;
        }

        // Constructor with one parameter for the top (numerator)
        public Fraction(int numerator)
        {
            _numerator = numerator;
            _denominator = 1;
        }

        // Constructor with two parameters for top (numerator) and bottom (denominator)
        public Fraction(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        // Getters and setters
        public int GetNumerator()
        {
            return _numerator;
        }

        public void SetNumerator(int numerator)
        {
            _numerator = numerator;
        }

        public int GetDenominator()
        {
            return _denominator;
        }

        public void SetDenominator(int denominator)
        {
            _denominator = denominator;
        }

        // Fraction methods
        public string GetFractionString()
        {
            return $"{_numerator}/{_denominator}";
        }

        public double GetDecimalValue()
        {
            return (double)_numerator / _denominator;
        }

    }
}