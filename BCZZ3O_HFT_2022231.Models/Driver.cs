﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace VehicleFleetDb.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }
        [Required]
        [StringLength(240)]
        public string Name { get; set; }
        [Range(21, 70)]
        public int Age { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Shift> Shifts { get; set; }

        public Driver()
        {

        }

        public Driver(string line)
        {
            string[] parts = line.Split('#');
            Name = parts[0];
            Age = int.Parse(parts[1]);
        }

        public override string ToString()
        {
            return $"{DriverId} - {Name} - {Age}";
        }
    }
}
