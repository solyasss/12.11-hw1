using System;
using System.Collections.Generic;
using System.Linq;

namespace hw
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
    }

    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main()
        {
            List<Department> departments = new List<Department>()
            {
                new Department(){ Id = 1, Country = "Ukraine", City = "Odesa" },
                new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = " Ukraine ", City = "Lviv" }
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
                new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
                new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
                new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
                new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
                new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
                new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
            };

            //1
            // linq
            Console.WriteLine("Employees in Ukraine");
            var employees_ukr = (from emp in employees
                                 join dep in departments on emp.DepId equals dep.Id
                                 where dep.Country.Trim() == "Ukraine"
                                 orderby emp.FirstName, emp.LastName
                                 select emp).ToList();

            foreach (var emp in employees_ukr)
            {
                Console.WriteLine($"- {emp.FirstName} {emp.LastName}");
            }

            // linq method
            var employees_ukr_2 = employees
                .Join(departments,
                      emp => emp.DepId,
                      dep => dep.Id,
                      (emp, dep) => new { emp, dep })
                .Where(x => x.dep.Country.Trim() == "Ukraine")
                .OrderBy(x => x.emp.FirstName)
                .ThenBy(x => x.emp.LastName)
                .Select(x => x.emp)
                .ToList();

            foreach (var emp in employees_ukr_2)
            {
                Console.WriteLine($"- {emp.FirstName} {emp.LastName}");
            }

            //2
            // linq
            Console.WriteLine("\nEmployees sorted by age");
            var employees_query = (from emp in employees
                                   orderby emp.Age descending
                                   select new { emp.Id, emp.FirstName, emp.LastName, emp.Age }).ToList();

            foreach (var emp in employees_query)
            {
                Console.WriteLine($"ID: {emp.Id} | Name: {emp.FirstName} {emp.LastName} | Age: {emp.Age}");
            }

            // linq method
            var employees_query_2 = employees
                .OrderByDescending(emp => emp.Age)
                .Select(emp => new { emp.Id, emp.FirstName, emp.LastName, emp.Age })
                .ToList();

            foreach (var emp in employees_query_2)
            {
                Console.WriteLine($"ID: {emp.Id} | Name: {emp.FirstName} {emp.LastName} | Age: {emp.Age}");
            }

            //3
            // linq
            Console.WriteLine("\nGrouped employees by age ");
            var group_query = (from emp in employees
                               group emp by emp.Age into group_age
                               select new { Age = group_age.Key, Count = group_age.Count() }).ToList();

            foreach (var group in group_query)
            {
                Console.WriteLine($"Age: {group.Age} | Total employees: {group.Count}");
            }

            // linq method
            var group_query_2 = employees
                .GroupBy(emp => emp.Age)
                .Select(group_age => new { Age = group_age.Key, Count = group_age.Count() })
                .ToList();

            foreach (var group in group_query_2)
            {
                Console.WriteLine($"Age: {group.Age} | Total employees: {group.Count}");
            }
        }
    }
}
