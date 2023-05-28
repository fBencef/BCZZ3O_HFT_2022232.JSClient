using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetDb.Models
{
    public class Shift
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ShiftId { get; set; }
        [StringLength(240)]
        public string FromYard { get; set; }
        public DateTime Date { get; set; }
        [StringLength(4)]
        public string Line { get; set; }
        [StringLength(4)]
        public string Tour { get; set; }
        [ForeignKey(nameof(Driver))]
        public int DriverId { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }
        [NotMapped]
        public virtual Driver Driver { get; set; }
        [NotMapped]
        public virtual Vehicle Vehicle { get; set; }

        public Shift()
        {

        }

        public Shift(string line)
        {
            string[] parts = line.Split('#');
            FromYard = parts[0];
            Date = DateTime.Parse(parts[1]);
            Line = parts[2];
            Tour = parts[3];
            DriverId = int.Parse(parts[4]);
            VehicleId = parts[5];
        }

        public override string ToString()
        {
            return $"{Line}/{Tour} - {Vehicle.Registration}";
        }
    }
}
