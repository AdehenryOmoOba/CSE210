using System;

namespace inheritance_demo
{
    public class Student
    {
        private string _name;

        public Student(string name)
        {
            _name = name;
        }

        public void sayName()
        {
            Console.WriteLine($"My name is: {_name}");
        }
    }
}
