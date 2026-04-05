using System;
using System.Collections.Generic;

namespace EmployeeTimeSystem
{
    abstract class TimeComputation
    {
        public abstract double CalculateHours(DateTime timeIn, DateTime timeOut);
        public abstract string GetNote(double hoursWorked);
    }

    class WorkHours : TimeComputation
    {
        public override double CalculateHours(DateTime timeIn, DateTime timeOut)
        {
            return (timeOut - timeIn).TotalHours;
        }

        public override string GetNote(double hoursWorked)
        {
            if (hoursWorked == 9)
                return "";
            else if (hoursWorked < 9)
                return $"Early Out. Hours left: {9 - hoursWorked:F0} hours";
            else
                return $"Overtime. Hours extended: {hoursWorked - 9:F0} hours";
        }
    }

    class Program
    {
        static DateTime GetTimeByLocation(string location)
        {
            if (location == "Philippines")
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Singapore Standard Time");
            else if (location == "United States")
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            else if (location == "India")
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            else
                throw new Exception("Invalid Location");
        }

        static void Main(string[] args)
        {
            
            Console.Write("Employee Number: ");
            string empNum = Console.ReadLine();

            Console.Write("Employee Name: ");
            string empName = Console.ReadLine();

            Console.Write("Which office are you located? (Philippines/United States/India): ");
            string location = Console.ReadLine();

            while (location != "Philippines" && location != "United States" && location != "India")
            {
                Console.Write("Invalid location. Please enter (Philippines/United States/India): ");
                location = Console.ReadLine();
            }

            
            DateTime timeIn = GetTimeByLocation(location);

            
            Console.WriteLine($"\nEmployee Number:");
            Console.WriteLine($"{empNum}");

            Console.WriteLine("Which office are you located?");
            Console.WriteLine($"{location}");

            Console.WriteLine("You clocked in at:");
            Console.WriteLine($"Date:{timeIn:MM/dd/yy}");
            Console.WriteLine($"Time: {timeIn:hh:mm:ss tt}");

            
            Console.WriteLine("\nEmployee Log:");
            Console.WriteLine($"Name:{empName}");
            Console.WriteLine($"Location:{location}");
            Console.WriteLine($"Time-In:{timeIn:MM/dd/yy hh:mm:ss tt}");

            
            Console.WriteLine("\nPress ENTER when employee clocks out...");
            Console.ReadLine();

            DateTime timeOut = GetTimeByLocation(location);

            Console.WriteLine("\nYou clocked out at:");
            Console.WriteLine($"Date:{timeOut:MM/dd/yy}");
            Console.WriteLine($"Time: {timeOut:hh:mm:ss tt}");

            
            WorkHours work = new WorkHours();
            double hoursWorked = work.CalculateHours(timeIn, timeOut);
            string note = work.GetNote(hoursWorked);

            string hoursDisplay = (hoursWorked == Math.Floor(hoursWorked)) 
                ? hoursWorked.ToString("F0") 
                : hoursWorked.ToString("F2");

            
            Console.WriteLine("\nEmployee Log:");
            Console.WriteLine($"Name:{empName}");
            Console.WriteLine($"Location:{location}");
            Console.WriteLine($"Time-In:{timeIn:MM/dd/yy hh:mm:ss tt}");
            Console.WriteLine($"Time-Out:{timeOut:MM/dd/yy hh:mm:ss tt}");
            Console.WriteLine($"Total Hours:{hoursDisplay}");

            if (!string.IsNullOrEmpty(note))
            {
                Console.WriteLine($"Note:{note}");
            }
        }
    }
}