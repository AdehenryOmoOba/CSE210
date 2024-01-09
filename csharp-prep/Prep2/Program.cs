using System;

class Program
{
    static void Main(string[] args)
    {
        // Core Requirement 1: Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = Convert.ToDouble(Console.ReadLine());

        // Core Requirement 2: Determine the letter grade and congratulate or encourage the user
        string letter;
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Stretch Challenge: Determine the grade sign
        string sign = "";
        if (letter != "F")
        {
            int lastDigit = (int)gradePercentage % 10;
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Stretch Challenge: Handle exceptional cases (A+, F+, and F- are not valid)
        if (letter == "A" && (sign == "+" || gradePercentage > 92))
        {
            sign = "";
        }
        else if (letter == "F" && sign != "")
        {
            sign = "";
        }

        // Stretch Challenge: Display both the grade letter and sign in one print statement
        Console.WriteLine($"Your final grade is: {letter}{sign}");
    }
}