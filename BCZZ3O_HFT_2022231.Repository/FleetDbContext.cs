using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Repository
{
    public class FleetDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public FleetDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseInMemoryDatabase("database").UseLazyLoadingProxies();
                
                //string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Repositories\VehicleFleet.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                //builder
                //.UseSqlServer(conn)
                //.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //REALTIONS
            modelBuilder.Entity<Shift>()
                          .HasOne(t => t.Vehicle)
                          .WithMany(t => t.Shifts)
                          .HasForeignKey(t => t.VehicleId)
                          .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shift>()
                          .HasOne(t => t.Driver)
                          .WithMany(t => t.Shifts)
                          .HasForeignKey(t => t.DriverId)
                          .OnDelete(DeleteBehavior.Cascade);
            //SEED CONTENT
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle[]
                {
                    new Vehicle("MHU863#Mercedes-Benz#Citaro C2#12#2014-05-01"),
                    new Vehicle("MHU859#Mercedes-Benz#Citaro C2#12#2014-05-01"),
                    new Vehicle("MHU811#Mercedes-Benz#Citaro C2#12#2014-05-01"),
                    new Vehicle("MHU723#Mercedes-Benz#Citaro C2G#18#2014-05-01"),
                    new Vehicle("MHU703#Mercedes-Benz#Citaro C2G#18#2014-05-01"),
                    new Vehicle("MHU719#Mercedes-Benz#Citaro C2G#18#2014-05-01"),
                    new Vehicle("NCA445#Mercedes-Benz#Conecto G#18#2015-08-12"),
                    new Vehicle("NCA459#Mercedes-Benz#Conecto G#18#2015-08-12"), //asp
                    new Vehicle("NCA531#Mercedes-Benz#Conecto G#18#2015-08-12"),
                    new Vehicle("SGY801#MAN#Lion's City A21#12#2020-12-07"),
                    new Vehicle("SGY806#MAN#Lion's City A21#12#2020-12-07"),
                    new Vehicle("AAMH601#MAN#Lion's City C18#18#2023-05-01"),
                    new Vehicle("AAMH602#MAN#Lion's City C18#18#2023-05-01"),
                    new Vehicle("AAHY824#MAN#Lion's City C12#12#2023-05-01"),
                    new Vehicle("AAHY825#MAN#Lion's City C12#12#2023-05-01"),
                });

            modelBuilder.Entity<Driver>().HasData(new Driver[]
                {
                    new Driver(){ DriverId = 1, Name="Kiss János", Age=45},
                    new Driver(){ DriverId = 2, Name="Lantos Johnny", Age=21},
                    new Driver(){ DriverId = 3, Name="Nagy Barna", Age=35},
                    new Driver(){ DriverId = 4, Name="Bácsi Pista", Age=66},
                    new Driver(){ DriverId = 5, Name="Nagy Imre", Age=46},
                    new Driver(){ DriverId = 6, Name="Farkas Gellért Máté", Age=27},
                    new Driver(){ DriverId = 7, Name="Fordító Zsófia", Age=26},
                    new Driver(){ DriverId = 8, Name="Kapucni Róbert", Age=32},
                    new Driver(){ DriverId = 9, Name="Kovács Lábtörlő", Age=40},
                    new Driver(){ DriverId = 10, Name="Szabó Korcsolya", Age=48},
                    new Driver(){ DriverId = 11, Name="Lakó Dávid", Age=51},
                    new Driver(){ DriverId = 12, Name="Lakatos Alehandro", Age=30},
                    new Driver(){ DriverId = 13, Name="Kis Barna", Age=24},
                    new Driver(){ DriverId = 14, Name="Vadász Levente", Age=31}, 
                    new Driver(){ DriverId = 15, Name="Fekete Fehér", Age=27}, 
                    new Driver(){ DriverId = 16, Name="Mező Pista", Age=46}, 
                    new Driver(){ DriverId = 17, Name="Zöld Alma", Age=36}, 
                });

            modelBuilder.Entity<Shift>().HasData(new Shift[]
                {
                    new Shift(){ ShiftId = 1, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="120",Tour="F55",DriverId=1,VehicleId="MHU863" },
                    new Shift(){ ShiftId = 2, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="296",Tour="F54",DriverId=2,VehicleId="MHU859" },
                    new Shift(){ ShiftId = 3, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="5",Tour="F65",DriverId=3,VehicleId="MHU723" },
                    new Shift(){ ShiftId = 4, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="296",Tour="F55",DriverId=4,VehicleId="MHU811" },
                    new Shift(){ ShiftId = 5, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="106",Tour="F60",DriverId=5,VehicleId="MHU703" },
                    new Shift(){ ShiftId = 6, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-11-02"),Line="5",Tour="F54",DriverId=6,VehicleId="MHU719" },
                    new Shift(){ ShiftId = 7, FromYard = "Andor utca", Date=DateTime.Parse("2022-11-02"),Line="100E",Tour="F36",DriverId=9,VehicleId="NCA445" },
                    new Shift(){ ShiftId = 8, FromYard = "Andor utca", Date=DateTime.Parse("2022-11-02"),Line="100E",Tour="F82",DriverId=10,VehicleId="NCA531" },
                    new Shift(){ ShiftId = 9, FromYard = "Andor utca", Date=DateTime.Parse("2022-11-02"),Line="153",Tour="F46",DriverId=8,VehicleId="SGY801" },
                    new Shift(){ ShiftId = 10, FromYard = "Andor utca", Date=DateTime.Parse("2022-11-02"),Line="58",Tour="F33",DriverId=12,VehicleId="SGY806" },
                    new Shift(){ ShiftId = 11, FromYard = "Bogáncs utca", Date=DateTime.Parse("2023-05-29"),Line="196",Tour="F51",DriverId=17,VehicleId="AAMH601" },
                    new Shift(){ ShiftId = 12, FromYard = "Bogáncs utca", Date=DateTime.Parse("2023-05-29"),Line="196",Tour="F52",DriverId=16,VehicleId="AAMH602" },
                    new Shift(){ ShiftId = 13, FromYard = "Bogáncs utca", Date=DateTime.Parse("2023-05-26"),Line="220",Tour="F54",DriverId=15,VehicleId="AAHY824" },
                    new Shift(){ ShiftId = 14, FromYard = "Bogáncs utca", Date=DateTime.Parse("2023-05-26"),Line="220",Tour="F55",DriverId=14,VehicleId="AAHY825" },
                }) ;
        }

    }
}
