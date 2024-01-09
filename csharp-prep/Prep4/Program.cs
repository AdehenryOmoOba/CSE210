using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Core Requirement: Ask the user for a series of numbers and append each one to a list
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int input;
        do
        {
            Console.Write("Enter number: ");
            input = Convert.ToInt32(Console.ReadLine());

            if (input != 0)
                numbers.Add(input);

        } while (input != 0);

        // Core Requirement 1: Compute the sum of the numbers in the list
        int sum = numbers.Sum();

        // Core Requirement 2: Compute the average of the numbers in the list
        double average = numbers.Average();

        // Core Requirement 3: Find the maximum number in the list
        int maxNumber = numbers.Max();

        // Stretch Challenge 1: Find the smallest positive number
        int smallestPositive = numbers.Where(x => x > 0).DefaultIfEmpty(0).Min();

        // Stretch Challenge 2: Sort the list
        List<int> sortedList = numbers.OrderBy(x => x).ToList();

        // Display the results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        Console.WriteLine("The sorted list is:");

        foreach (var num in sortedList)
        {
            Console.WriteLine(num);
        }
    }
}