using System;

class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
            // Core Requirement 1: Ask the user for the magic number
            Console.WriteLine("What is the magic number? ");
            int magicNumber = new Random().Next(0, 100);

            // Stretch Challenge: Keep track of the number of guesses
            int numberOfGuesses = 0;

            // Core Requirement 2: Add a loop that keeps looping until the guess matches the magic number
            int userGuess = -1;

            while (userGuess != magicNumber)
            {
                // Core Requirement 3: Ask the user for a guess
                Console.Write("What is your guess? ");
                userGuess = Convert.ToInt32(Console.ReadLine());

                // Core Requirement 4: Determine if the user needs to guess higher or lower
                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }

                numberOfGuesses++;
            }

            // Stretch Challenge: Inform the user of the number of guesses
            Console.WriteLine($"It took you {numberOfGuesses} guesses to find the magic number.");

            // Stretch Challenge: Ask the user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();

            // Check if the user wants to play again
            playAgain = (playAgainInput == "yes");
        }
    }
}