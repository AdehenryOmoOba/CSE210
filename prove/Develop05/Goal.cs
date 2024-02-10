using System;
using System.Collections.Generic;
using System.IO;

namespace eternal_quest
{
    abstract class Goal
    {
        protected string _shortName;
        protected string _description;
        public int _points;
        private int _currentScore;
        public string _goalType;

        public Goal(string shortName, string description, int points, string goalType, int currentScore = 0)
        {
            _shortName = shortName;
            _description = description;
            _points = points;
            _currentScore = currentScore;
            _goalType = goalType;
        }


        public void SetCurrentScore(int points)
        {
            _currentScore = points;
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }
        public abstract void RecordEvent();

        public abstract bool IsComplete();

        public abstract string GetDetailsString();

        public abstract string GetStringRepresentation();
    }
}