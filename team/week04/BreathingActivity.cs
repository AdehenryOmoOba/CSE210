using System;

namespace mindfulness_app
{
    class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity(int duration) : base("Breathing Activity", "Walk through breathing in and out slowly.", duration) { }

        public void Run()
        {
            DisplayStartingMessage();
            ShowCountDown(3);

            Console.WriteLine("Clear your mind and focus on your breathing...");

            for (int i = 0; i < Duration; i += 6)
            {
                Console.WriteLine("Breathe in...");
                Thread.Sleep(3000);
                Console.WriteLine("Breathe out...");
                Thread.Sleep(3000);
            }

            DisplayEndingMessage();
            ShowSpinner(3);
            UpdateActivityLog();
        }
    }
}