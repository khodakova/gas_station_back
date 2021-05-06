using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DeliveryModel : ParentModel
    {
        [Display(Name = "Создатель заявки на поставку")]
        public int CreateBy { get; set; }
        public EmployeeModel Employee { get; set; }
        public List<DeliveryItemModel> DeliveryItems { get; set; } = new List<DeliveryItemModel>();
        [Display(Name = "Статус поставки (0 - зарегистрирована, 1 - взята в работу, 2 - завершена)")]
        public bool Status { get; set; }
        [Display(Name = "Стоимость")]
        public int Summ { get; set; }
        [Display(Name = "Принял поставку")]
        public int EndBy { get; set; }
        [Display(Name = "Дата завершения")]
        public DateTime EndDate { get; set; }

    }
}
