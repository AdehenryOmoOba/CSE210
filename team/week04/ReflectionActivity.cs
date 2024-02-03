using System;

namespace mindfulness_app
{
    class ReflectiveActivity : MindfulnessActivity
    {
        private readonly List<string> prompts;
        private readonly List<string> questions;

        public ReflectiveActivity(List<string> prompts, List<string> questions) : base("Reflective Activity", "This activity helps you reflect on life key moments.", 3)
        {
            this.prompts = prompts;
            this.questions = questions;
        }

        public void Run()
        {
            DisplayStartingMessage();
            GetRandomPrompt();
            GetRandomQuestion();
            DisplayPrompt();
            DisplayQuestion();
            DisplayEndingMessage();
            ShowSpinner(3);
            UpdateActivityLog();
        }

        private void GetRandomPrompt()
        {
            Console.WriteLine($"Prompt: {prompts[0]}");
        }

        private void GetRandomQuestion()
        {
            Console.WriteLine($"Question: {questions[0]}");
        }

        private void DisplayPrompt()
        {
            Console.WriteLine("Reflect on the prompt...");
            // Simulating a pause for reflection
            Thread.Sleep(5000);
        }

        private void DisplayQuestion()
        {
            Console.WriteLine("Reflect on the question...");
            // Simulating a pause for reflection
            Thread.Sleep(5000);
        }
    }
}