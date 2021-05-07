using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class Storage : Parent
    {
        [Display(Name = "Единица топлива")]
        public List<Fuel> Fuels { get; set; } = new List<Fuel>();
        [Display(Name = "Имеющийся объем")]
        public float Value { get; set; }
        [Display(Name = "Статус")]
        public bool Status { get; set; }
    }
}
