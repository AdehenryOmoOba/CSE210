using System;
using System.Collections.Generic;
using System.Threading;


namespace mindfulness_app
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Mindfulness Program");

            while (true)
            {
                Console.WriteLine("\nChoose an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Listing Activity");
                Console.WriteLine("3. Reflective Activity");
                Console.WriteLine("4. View Activity Log");
                Console.WriteLine("5. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (choice == 5)
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                if (choice == 4)
                {
                    DisplayTotalActivityLog();
                    continue;
                }

                Console.WriteLine("Enter the duration (in seconds):");
                if (!int.TryParse(Console.ReadLine(), out int duration))
                {
                    Console.WriteLine("Invalid input. Please enter a number for duration.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        new BreathingActivity(duration).Run();
                        break;
                    case 2:
                        List<string> prompts = new List<string> { "Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?" };
                        new ListingActivity(duration, prompts).Run();
                        break;
                    case 3:
                        List<string> reflectivePrompts = new List<string> { "Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless." };
                        List<string> reflectiveQuestions = new List<string> { "Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?" };
                        new ReflectiveActivity(reflectivePrompts, reflectiveQuestions).Run();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid activity.");
                        break;
                }
            }
        }

        private static void DisplayTotalActivityLog()
        {
            Console.WriteLine("\nTotal Activity Log:");
            foreach (var entry in MindfulnessActivity.ActivityLog)
            {
                Console.WriteLine($"{entry.Key.Name}: {entry.Value} times");
            }
        }
    }

}