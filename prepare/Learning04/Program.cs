using System;

namespace inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            MathAssignment math101 = new MathAssignment("Adehenry", "Algebra", "4.1", "x-2y");

            string mathSummary = math101.GetSummary();
            string homeworkList = math101.GetHomeworkList();

            Console.WriteLine(mathSummary);
            Console.WriteLine(homeworkList);

            WritingAssignment eng101 = new WritingAssignment("OmoOba", "Articles", "The gods are not to blame");

            string engSummary = eng101.GetSummary();
            string writingInfo = eng101.GetWritingInformation();

            Console.WriteLine(engSummary);
            Console.WriteLine(writingInfo);
        }
    }
}