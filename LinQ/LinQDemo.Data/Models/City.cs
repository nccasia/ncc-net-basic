using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQDemo.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<County> Counties { get; set; }
    }
}
