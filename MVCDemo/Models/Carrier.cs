using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Table("Carriers")]
    public class Carrier
    {
        public Carrier()
        {
            this.Cargoes = new List<Cargo>();
        }
        [Key]
        public int CarrierId { get; set; }

        public string Name { get; set; }
        [EmailAddress]
        public string Emeil { get; set; }

        public long CarrierIdentificator { get; set; }

        public ICollection<Cargo> Cargoes { get; set; }
    }
}