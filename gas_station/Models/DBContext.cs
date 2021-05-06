using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DBContext : DbContext
    {
        public DbSet<StationModel> StationModels { get; set; }

        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
            if (!StationModels.Any())
            {
                StationModels.Add(new StationModel
                {
                    name = "ёпта",
                    note = "атата"
                });
                StationModels.Add(new StationModel
                {
                    name = "ёпта1",
                    note = "атата1"
                });
                SaveChanges();
            }
        }
    }
}
