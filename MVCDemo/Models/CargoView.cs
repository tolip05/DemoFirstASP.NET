using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class CargoView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PricePerKilometer { get; set; }

        public int Weight { get; set; }

        public string FromLocation{ get; set; }


        public string ForLocation { get; set; }

        public string CargoType { get; set; }

        public string TransportType { get; set; }

        public string Forwarder { get; set; }

    }
}