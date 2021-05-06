using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class StorageModel : ParentModel
    {
        [Display(Name = "Единица топлива")]
        public List<FuelModel> Fuels { get; set; } = new List<FuelModel>();
        [Display(Name = "Имеющийся объем")]
        public float Value { get; set; }
        [Display(Name = "Статус")]
        public bool Status { get; set; }
    }
}
