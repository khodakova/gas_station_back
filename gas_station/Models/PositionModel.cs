using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class PositionModel : ParentModel
    {
        [Display(Name = "Название")]
        public override string Name { get => base.Name; set => base.Name = value; }
        public EmployeeModel Employee { get; set; }
        public override DateTime CreateDate { get => base.CreateDate; set => base.CreateDate = value; }
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
