using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQDemo.Data.Models
{
    public class School
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }

        [ForeignKey(nameof(CountyId))]
        public virtual County County { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
