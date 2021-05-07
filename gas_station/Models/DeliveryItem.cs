using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DeliveryItem : Parent
    {
        [Display(Name = "Название единицы поставки")]
        public string Name { get; set; }

        [Display(Name = "Id поставки")]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        [Display(Name = "Объем")]
        public float Value { get; set; }

        [Display(Name = "Цена")]
        public float Price { get; set; }

        [Display(Name = "Сумма")]
        public float Summ { get; set; }

        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        
    }
}
