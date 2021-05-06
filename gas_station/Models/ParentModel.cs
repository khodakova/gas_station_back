using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class ParentModel
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }

    }
}
