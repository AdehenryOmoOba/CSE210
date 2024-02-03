using System;

namespace mindfulness_app
{
    class ListingActivity : MindfulnessActivity
    {
        public int Count { get; private set; }
        private readonly List<string> prompts;

        public ListingActivity(int duration, List<string> prompts) : base("Listing Activity", "List as many things as you can in a certain area.", duration)
        {
            Count = duration;
            this.prompts = prompts;
        }

        public void Run()
        {
            DisplayStartingMessage();
            ShowCountDown(5);
            GetRandomPrompt();
            GetListFromUser();
            DisplayEndingMessage();
            ShowSpinner(3);
            UpdateActivityLog();
        }

        private void GetRandomPrompt()
        {
            Console.WriteLine($"Prompt: {prompts[0]}");
        }

        private void GetListFromUser()
        {
            DateTime startTime = DateTime.Now;

            DateTime futureTime = startTime.AddSeconds(Count);


            for (int i = 1; i <= Duration; i++)
            {
                Console.Write($"{i}. ");
                // Simulating user input for each item
                string userInput = Console.ReadLine() ?? "";
                // You can store or process the user input as needed

                DateTime currentTime = DateTime.Now;

                if (currentTime >= futureTime)
                {
                    break;
                }

            }
        }
    }

}