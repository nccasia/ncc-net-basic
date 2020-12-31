using LinQDemo.Common;
using LinQDemo.Data;
using LinQDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQDemo.Service
{
    public class StudentService : ServiceBase, IStudentService
    {
        public StudentService(LinQDemoContext context) : base(context)
        {
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return Context.Students.Where(predicate);
        }

        public IEnumerable<Student> GetAll()
        {
            return Context.Students;
        }

        public IEnumerable<Student> GetByClass(long classId)
        {
            return Find(i => i.ClassId == classId);
        }

        public IEnumerable<Student> GetByGrade(int grade)
        {
            return Context.Students.Include(i => i.Class).Where(i => i.Class.Grade == grade);
        }
    }
}
