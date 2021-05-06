using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class StationModel
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Название")]
        public string name { get; set; }
        public int placeId { get; set; }
        [Display(Name = "Комментарий")]
        public string note { get; set; }
        public bool status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

    }
}
