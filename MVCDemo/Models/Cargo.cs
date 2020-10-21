using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Table("Cargoes")]
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }

        public string Name { get; set; }

        public double PricePerKilometer { get; set; }

        public int FromLocationId { get; set; }

        public Location FromLocation { get; set; }

        public int ForLocationId { get; set; }

        public Location ForLocation { get; set; }

        public CargoType CargoType { get; set; }

        public TransportType TransportType { get; set; }

        public int ForwarderId { get; set; }
        public Forwarder Forwarder { get; set; }

        public int? CarrierId { get; set; }
        public Carrier Carrier { get; set; }
    }
}