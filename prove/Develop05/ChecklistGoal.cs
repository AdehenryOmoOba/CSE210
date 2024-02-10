using System;
using System.Collections.Generic;
using System.IO;

namespace eternal_quest
{
    class ChecklistGoal : Goal
    {
        public int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string shortName, string description, int points, int target, int bonus, int currentScore = 0) : base(shortName, description, points, "checkListGoal", currentScore)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        public override void RecordEvent()
        {
            _amountCompleted++;

            Console.WriteLine($"Amount completed is now: {_amountCompleted}");
            Console.WriteLine($"Total points is now: {_amountCompleted * _points}");

            int newSore = _amountCompleted * _points;

            SetCurrentScore(newSore);

            if (_amountCompleted == _target)
            {
                _points += _bonus;
            }
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public override string GetDetailsString()
        {
            return $"{_shortName} ({_description}) - Completed {_amountCompleted}/{_target} times";
        }

        public override string GetStringRepresentation()
        {
            return $"{nameof(ChecklistGoal)}:{_shortName},{_description},{_points},{_amountCompleted},{_target},{_bonus},{GetCurrentScore()}";
        }
    }
}
