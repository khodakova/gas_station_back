using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Storage> Storage { get; set; }

        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            if (!Stations.Any())
            {
                Stations.Add(new Station
                {
                    Name = "Центральная станция BirstTime",
                    Address = "г. Апчхи, Центральная улица, дом 132",
                    Status = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                Stations.Add(new Station
                {
                    Name = "BirstTime станция 1",
                    Address = "г. Апчхи, Шелковая улица, дом 583",
                    Status = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                SaveChanges();
            };
            if (!Positions.Any())
            {
                Positions.Add(new Position
                {
                    Name = "Администратор",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                Positions.Add(new Position
                {
                    Name = "Сотрудник",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                SaveChanges();
            };
            if (!Employees.Any())
            {
                Employees.Add(new Employee
                {
                    FirstName = "Администратор",
                    LastName = "Администратов",
                    MiddleName = "Администратович",
                    Birthdate = new DateTime(1995, 1, 1),
                    Code = 1111,
                    Status = true,
                    PositionId = 1,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                Employees.Add(new Employee
                {
                    FirstName = "Недерживсебе",
                    LastName = "Пупков",
                    MiddleName = "Прокопьевич",
                    Code = 1112,
                    Birthdate = new DateTime(1997, 1, 1),
                    Status = true,
                    PositionId = 2,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                SaveChanges();
            };
            
        }
    }
}
