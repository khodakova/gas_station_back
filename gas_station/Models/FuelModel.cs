using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class FuelModel : ParentModel
    {
        [Display(Name = "Название топлива")]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Display(Name = "Тип топлива")]
        public int FuelTypeId { get; set; }
        public FuelTypeModel FuelType { get; set; }
        [Display(Name = "Id единицы поставки")]
        public int DeliveryItemId { get; set; }
        public DeliveryItemModel DeliveryItem { get; set; }
        [Display(Name = "Id записи в прайсе")]
        public PriceListModel PriceList { get; set; }
    }
}
