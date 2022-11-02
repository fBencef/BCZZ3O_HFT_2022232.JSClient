﻿using Microsoft.EntityFrameworkCore;
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
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Repositories\VehicleFleet.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                builder
                .UseSqlServer(conn)
                .UseLazyLoadingProxies();

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //REALTIONS
            modelBuilder.Entity<Shift>(shift => shift
                          .HasOne<Vehicle>()
                          .WithMany()
            .HasForeignKey(shift => shift.VehicleId));
            modelBuilder.Entity<Shift>(shift => shift
                           .HasOne<Driver>()
                           .WithMany()
                           .HasForeignKey(shift => shift.DriverId));
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
                    new Vehicle("SGY801#MAN#Lion's City A21#18#2020-12-07"),
                    new Vehicle("SGY806#MAN#Lion's City A21#18#2020-12-07"),
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
                }) ;
        }

    }
}
