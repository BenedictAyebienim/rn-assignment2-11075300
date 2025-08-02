using System;

namespace GradeTicketTriangleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== DCIT318 Assignment 1 Menu ===");
                Console.WriteLine("1. Grade Calculator");
                Console.WriteLine("2. Ticket Price Calculator");
                Console.WriteLine("3. Triangle Type Identifier");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GradeCalculator();
                        break;
                    case "2":
                        TicketPriceCalculator();
                        break;
                    case "3":
                        TriangleTypeIdentifier();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void GradeCalculator()
        {
            Console.Write("\nEnter your grade (0 - 100): ");
            if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 0 && grade <= 100)
            {
                if (grade >= 90)
                    Console.WriteLine("Letter Grade: A");
                else if (grade >= 80)
                    Console.WriteLine("Letter Grade: B");
                else if (grade >= 70)
                    Console.WriteLine("Letter Grade: C");
                else if (grade >= 60)
                    Console.WriteLine("Letter Grade: D");
                else
                    Console.WriteLine("Letter Grade: F");
            }
            else
            {
                Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
            }
        }

        static void TicketPriceCalculator()
        {
            Console.Write("\nEnter your age: ");
            if (int.TryParse(Console.ReadLine(), out int age) && age > 0)
            {
                double price = (age <= 12 || age >= 65) ? 7.00 : 10.00;
                Console.WriteLine($"Ticket Price: GHC{price}");
            }
            else
            {
                Console.WriteLine("Invalid age. Please enter a valid number.");
            }
        }

        static void TriangleTypeIdentifier()
        {
            Console.WriteLine("\nEnter the lengths of the three sides of the triangle:");

            double[] sides = new double[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Side {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out sides[i]) || sides[i] <= 0)
                {
                    Console.WriteLine("Invalid input. All sides must be positive numbers.");
                    return;
                }
            }

            double a = sides[0], b = sides[1], c = sides[2];

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                Console.WriteLine("These sides do not form a valid triangle.");
                return;
            }

            if (a == b && b == c)
                Console.WriteLine("Triangle Type: Equilateral");
            else if (a == b || a == c || b == c)
                Console.WriteLine("Triangle Type: Isosceles");
            else
                Console.WriteLine("Triangle Type: Scalene");
        }
    }
}
