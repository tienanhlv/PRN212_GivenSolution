using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseTitle { get; set; }

        private Dictionary<Student, double> students;


        public Course(int courseID, string courseTitle)
        {
            CourseID = courseID;
            CourseTitle = courseTitle;
            students = new Dictionary<Student, double>();
        }

        public void AddStudent(Student p, double g)
        {
            if (!students.ContainsKey(p))
            {
                int oldCount = students.Count;
                students.Add(p, g);
                int newCount = students.Count;
                OnNumberOfStudentChange?.Invoke(oldCount, newCount);
            }
        }

        public void RemoveStudent(int StudentID)
        {
            Student studentToRemove = null;
            foreach (var student in students.Keys)
            {
                if (student.StudentID == StudentID)
                {
                    studentToRemove = student;
                    break;
                }
            }

            if (studentToRemove != null)
            {
                int oldCount = students.Count;
                students.Remove(studentToRemove);
                int newCount = students.Count;
                OnNumberOfStudentChange?.Invoke(oldCount, newCount);
            }
        }

        public override string ToString()
        {
            string result = $"Course: {CourseID} - {CourseTitle}\n";
            foreach (var entry in students)
            {
                result += $"Student: {entry.Key.StudentID} - {entry.Key.StudentName} - Mark: {entry.Value}\n";
            }
            return result;
        }

        public delegate void StudentChangeHandler(int oldNumber, int newNumber);

        public event StudentChangeHandler OnNumberOfStudentChange;
    }
}
