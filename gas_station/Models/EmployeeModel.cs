using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class EmployeeModel : ManModel
    {
        [Display(Name = "Табельный номер")]
        public int Code { get; set; }
        public int ManId { get; set; }
        public ManModel Man { get; set; }
        public List<StationModel> Stations { get; set; } = new List<StationModel>();
        public int PositionId { get; set; }
        public PositionModel Position { get; set; }
        public override DateTime CreateDate { get => base.CreateDate; set => base.CreateDate = value; }
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }

    }
}
