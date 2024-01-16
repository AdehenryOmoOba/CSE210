using System;
using System.Collections.Generic;


class Program
{
    public class Job
    {
        // Member variables for Job class
        public string _jobTitle;
        public string _company;
        public int _startYear;
        public int _endYear;

        // Constructor to initialize Job object
        public Job()
        {
            _jobTitle = null;
            _company = null;
            _startYear = 0;
            _endYear = 0;
        }

        // Display method to show job details
        public void Display()
        {
            Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
        }
    }

    public class Resume
    {
        // Member variables for Resume class
        public string _personName;
        public List<Job> _jobs;

        // Constructor to initialize Resume object
        public Resume(string personName)
        {
            _personName = personName;
            _jobs = new List<Job>();
        }

        // Display method to show resume details
        public void Display()
        {
            Console.WriteLine($"Name: {_personName}\nJobs:");
            foreach (var job in _jobs)
            {
                job.Display();
            }
        }
    }
    static void Main(string[] args)
    {
        // Create Job instances
        Job job1 = new Job();

        job1._jobTitle = "Software Engineer";
        job1._company = "Microsoft";
        job1._startYear = 2019;
        job1._endYear = 2022;

        Job job2 = new Job();

        job2._jobTitle = "Manager";
        job2._company = "Apple";
        job2._startYear = 2022;
        job2._endYear = 2023;

        // Display jobs
        Console.WriteLine(job1._company);
        Console.WriteLine(job2._company);

        Console.WriteLine();

        // Display job details using the Display method
        job1.Display();
        job2.Display();

        Console.WriteLine();

        // Create Resume instance
        Resume myResume = new Resume("Allison Rose");

        // Add jobs to the list of jobs in the resume
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        // Display resume details using the Display method
        myResume.Display();
    }
}
