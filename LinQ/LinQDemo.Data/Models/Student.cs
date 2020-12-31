using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQDemo.Data.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public long? SchoolId { get; set; }
        public long? ClassId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
    }
}