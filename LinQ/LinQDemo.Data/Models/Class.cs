using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQDemo.Data.Models
{
    public class Class
    {
        public long Id { get; set; }
        public int Grade { get; set; }
        public string Name { get; set; }
        public long? SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public virtual School School { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
