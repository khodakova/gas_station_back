using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class OrderModel : ParentModel
    {
        [Display(Name = "Создатель заказа")]
        public int CreateBy { get; set; }
        public UserModel User { get; set; }
        [Display(Name = "На кого заказ")]
        public int ManId { get; set; }
        public ManModel Man { get; set; }
        [Display(Name = "Единица топлива")]
        public int FuelId { get; set; }
        public FuelModel Fuel { get; set; }
        [Display(Name = "Объем")]
        public float Value { get; set; }
        [Display(Name = "Скидка")]
        public float Discount { get; set; }
        [Display(Name = "Цена")]
        public float Price { get; set; }
        [Display(Name = "Сумма")]
        public float Summ { get; set; }
    }
}
