using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    public class Student
    {
        public Student()
        {
        }

        public int StudentID {  get; set; }
        public string StudentName { get; set; }

        public Student(int studentID, string studentName)
        {
            StudentID = studentID;
            StudentName = studentName;
        }
    }
}
