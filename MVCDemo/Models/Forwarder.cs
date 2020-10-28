using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Table("Forwarders")]
    public class Forwarder
    {
        public Forwarder()
        {
            this.Cargoes = new List<Cargo>();
        }
        [Key]
        public int ForwarderId { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Emeil { get; set; }

        public long ForwarderIdentificator { get; set; }

        public ICollection<Cargo> Cargoes { get; set; }
    }
}