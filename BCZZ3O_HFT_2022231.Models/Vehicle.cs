using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleFleetDb.Models
{
    public class Vehicle
    {
        [Key]
        [Required]
        [StringLength(7)]
        public string Registration { get; set; }

        public string DisplayReg { get; set; }

        [StringLength(240)]
        public string Manufacturer { get; set; }
        [StringLength(240)]
        public string Model { get; set; }
        [Range(10, 20)]
        public int Length { get; set; }
        public DateTime RegistrationDate { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Shift> Shifts { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(string line)
        {
            string[] parts = line.Split('#');
            Registration = parts[0];
            DisplayReg = Registration;
            Manufacturer = parts[1];
            Model = parts[2];
            Length = int.Parse(parts[3]);
            RegistrationDate = DateTime.Parse(parts[4]);
        }

        public override string ToString()
        {
            return $"{DisplayReg} - {Manufacturer} - {Model} - {Length}m";
        }
    }
}
