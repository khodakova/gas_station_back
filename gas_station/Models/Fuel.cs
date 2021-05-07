using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class Fuel : Parent
    {
        [Display(Name = "Название топлива")]
        public string Name { get; set; }
        [Display(Name = "Тип топлива")]
        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        [Display(Name = "Id единицы поставки")]
        public DeliveryItem DeliveryItem { get; set; }
 //       [Display(Name = "Id записи в прайсе")]
 //       public PriceList PriceList { get; set; }
    }
}
