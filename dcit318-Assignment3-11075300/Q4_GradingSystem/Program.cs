using System;
using System.Collections.Generic;
using System.Linq;

namespace Q4_UniversityCourseManagement
{
    // a) Abstract class Course
    public abstract class Course
    {
        public string CourseCode;
        public string CourseTitle;
        public int CreditHours;

        public Course(string code, string title, int credits)
        {
            CourseCode = code;
            CourseTitle = title;
            CreditHours = credits;
        }

        public abstract void DisplayCourseInfo();
    }

    // b) UndergraduateCourse
    public class UndergraduateCourse : Course
    {
        public string Level; // e.g., "100", "200"

        public UndergraduateCourse(string code, string title, int credits, string level)
            : base(code, title, credits)
        {
            Level = level;
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"[Undergraduate] {CourseCode} - {CourseTitle}, Credits: {CreditHours}, Level: {Level}");
        }
    }

    // c) PostgraduateCourse
    public class PostgraduateCourse : Course
    {
        public string ResearchArea;

        public PostgraduateCourse(string code, string title, int credits, string researchArea)
            : base(code, title, credits)
        {
            ResearchArea = researchArea;
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"[Postgraduate] {CourseCode} - {CourseTitle}, Credits: {CreditHours}, Research Area: {ResearchArea}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // d) List of courses
            List<Course> courses = new()
            {
                new UndergraduateCourse("CS101", "Introduction to Programming", 3, "100"),
                new UndergraduateCourse("CS201", "Data Structures", 3, "200"),
                new UndergraduateCourse("CS301", "Operating Systems", 3, "300"),
                new PostgraduateCourse("CS501", "Advanced Machine Learning", 4, "Artificial Intelligence"),
                new PostgraduateCourse("CS502", "Distributed Systems", 4, "Computer Systems"),
                new PostgraduateCourse("CS503", "Natural Language Processing", 4, "Artificial Intelligence")
            };

            Console.WriteLine("=== All Courses ===");
            foreach (var course in courses)
            {
                course.DisplayCourseInfo();
            }

            // e) Filter by research area using LINQ
            Console.Write("\nEnter research area to filter postgraduate courses: ");
            string? area = Console.ReadLine();

            var filtered = courses
                .OfType<PostgraduateCourse>()
                .Where(c => c.ResearchArea.Equals(area, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine($"\n=== Postgraduate Courses in '{area}' ===");
            if (filtered.Count == 0)
            {
                Console.WriteLine("No courses found.");
            }
            else
            {
                foreach (var course in filtered)
                {
                    course.DisplayCourseInfo();
                }
            }
        }
    }
}

