using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class UserInput : User
    {
        public override string UserName { get; set; }
        public override string Password { get; set; }
        public override string Email { get; set; }
    }
}
