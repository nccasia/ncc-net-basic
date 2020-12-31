using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQDemo.Data.Models
{
    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public long NumberOfCitizens { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
