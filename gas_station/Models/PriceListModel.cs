using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class PriceListModel : ParentModel
    {
        [Display(Name = "Дата начала действия цены")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "Дата окончания действия цены")]
        public DateTime EndDate { get; set; }
        [Display(Name = "На какое топливо действует прайс")]
        public int FuelId { get; set; }
        public FuelModel Fuel { get; set; }
        [Display(Name = "Цена")]
        public float Price { get; set; }
        [Display(Name = "Статус")]
        public bool Status { get; set; }
        [Display(Name = "Комментарий")]
        public string Note { get; set; }
    }
}
