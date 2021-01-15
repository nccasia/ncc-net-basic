using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCSharp
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }

    public class Employee : Person
    {
        public decimal Salary { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();
            employee.FirstName = "Sander";
            employee.LastName = "Rossel";
            string fullName = employee.GetFullName();
            employee.Salary = 1000000;
        }
    }
}
