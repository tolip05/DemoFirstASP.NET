using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Table("Identificators")]
    public class Identificator
    {
        [Key]
        public int IdentificatorId { get; set; }

        public IdentificatorType IdentificatorType { get; set; }

        public int IdentificatorNumber { get; set; }
    }
}