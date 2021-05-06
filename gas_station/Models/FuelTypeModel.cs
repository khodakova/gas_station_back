using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class FuelTypeModel : ParentModel
    {
        [Display(Name = "Название типа топлива")]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
