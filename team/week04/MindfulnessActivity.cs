using System;

namespace mindfulness_app
{
    // Base class for all activities
    class MindfulnessActivity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Duration { get; protected set; }

        public static Dictionary<Type, int> ActivityLog = new Dictionary<Type, int>();

        public MindfulnessActivity(string name, string description, int duration)
        {
            Name = name;
            Description = description;
            Duration = duration;
        }

        public virtual void StartActivity()
        {
            DisplayStartingMessage();
            Thread.Sleep(3000); // Pause for starting message
            PerformActivity();
            DisplayEndingMessage();
            Thread.Sleep(3000); // Pause for ending message
            UpdateActivityLog();
        }

        protected virtual void DisplayStartingMessage()
        {
            Console.WriteLine($"Get ready for {Name.ToLower()}...");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Duration: {Duration} seconds.");
        }

        protected virtual void DisplayEndingMessage()
        {
            Console.WriteLine($"You've completed {Name.ToLower()}!");
        }

        protected virtual void PerformActivity()
        {
            // Implementation specific to each activity
        }

        protected void UpdateActivityLog()
        {
            Type activityType = GetType();
            if (ActivityLog.ContainsKey(activityType))
            {
                ActivityLog[activityType]++;
            }
            else
            {
                ActivityLog.Add(activityType, 1);
            }

            DisplayActivityLog();
        }

        private void DisplayActivityLog()
        {
            Console.WriteLine("\nActivity Log:");
            foreach (var entry in ActivityLog)
            {
                Console.WriteLine($"{entry.Key.Name}: {entry.Value} times");
            }
        }

        protected void ShowSpinner(int seconds)
        {
            Console.WriteLine("Processing...");
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(1000);
                Console.Write(". ");
            }
            Console.WriteLine();
        }

        protected void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Go!");
        }
    }
}