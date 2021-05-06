using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DeliveryItemModel : ParentModel
    {
        [Display(Name = "Название единицы поставки")]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Display(Name = "Id поставки")]
        public int DeliverId { get; set; }
        public DeliveryModel Delivery { get; set; }
        [Display(Name = "Объем")]
        public float Value { get; set; }
        [Display(Name = "Цена")]
        public float Price { get; set; }
        [Display(Name = "Сумма")]
        public float Summ { get; set; }
        public FuelModel Fuel { get; set; }
        
    }
}
