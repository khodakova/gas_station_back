using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DBContext : DbContext
    {
        public DbSet<StationModel> Stations { get; set; }
        public DbSet<DeliveryItemModel> DeliveryItems { get; set; }
        public DbSet<DeliveryModel> Deliveries { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<FuelModel> Fuels { get; set; }
        public DbSet<FuelTypeModel> FuelTypes { get; set; }
        public DbSet<ManModel> Men { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<PositionModel> Positions { get; set; }
        public DbSet<PriceListModel> PriceLists { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StorageModel> Storage { get; set; }

        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
            if (!Stations.Any())
            {
                Stations.Add(new StationModel
                {
                    Name = "ёпта",
                    Note = "атата"
                });
                Stations.Add(new StationModel
                {
                    Name = "ёпта1",
                    Note = "атата1"
                });
                SaveChanges();
            }
            if (!Men.Any())
            {
                Men.Add(new ManModel
                {
                    FirstName = "Администратор",
                    LastName = "Администратов",
                    MiddleName = "Администратович",
                    Birthdate = new DateTime(1995, 1, 1),
                    Status = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                Men.Add(new ManModel
                {
                    FirstName = "Недерживсебе",
                    LastName = "Пупков",
                    MiddleName = "Прокопьевич",
                    Birthdate = new DateTime(1997, 1, 1),
                    Status = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                SaveChanges();
            }
        }
    }
}
