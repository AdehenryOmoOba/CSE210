using System;

class Program
{
    static void Main(string[] args)
    {
        // Call DisplayWelcome function
        DisplayWelcome();

        // Call PromptUserName function and save the result
        string userName = PromptUserName();

        // Call PromptUserNumber function and save the result
        int userNumber = PromptUserNumber();

        // Call SquareNumber function with userNumber as a parameter and save the result
        int squaredNumber = SquareNumber(userNumber);

        // Call DisplayResult function with userName and squaredNumber as parameters
        DisplayResult(userName, squaredNumber);
    }

    // Function to display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Function to prompt the user for their name and return it as a string
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Function to prompt the user for their favorite number and return it as an integer
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return Convert.ToInt32(Console.ReadLine());
    }

    // Function to square a given number and return the result
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function to display the user's name and the squared number
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }
}
