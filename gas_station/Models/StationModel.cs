using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class StationModel : ParentModel
    {

        [Display(Name = "Название")]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Комментарий")]
        public string Note { get; set; }
        [Display(Name = "Статус")]
        public bool Status { get; set; }
        public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}
