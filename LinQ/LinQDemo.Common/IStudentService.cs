using LinQDemo.Data.Models;
using System;
using System.Collections.Generic;

namespace LinQDemo.Common
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        IEnumerable<Student> GetByGrade(int grade);
        IEnumerable<Student> GetByClass(long classId);
        IEnumerable<Student> Find(Func<Student, bool> predicate);
    }
}
