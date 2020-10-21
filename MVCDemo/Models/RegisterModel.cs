using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }

        public string TypeUser { get; set; }

        public string Emeil { get; set; }

        public long IdentidierNumber { get; set; }
    }
}