using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCDemo.Data
{
    public class MVCDemoContext : DbContext
    {
        public MVCDemoContext() : base("name=MVCDemoContext")
        {

        }

        public DbSet<Cargo> Cargoes { get; set; }

        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<Forwarder> Fowarders { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Identificator> Identificators { get; set; }



    }
}