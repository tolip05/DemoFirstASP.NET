using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class Covid
    {
        public string Country { get; set; }
        public Roo Roo { get; set; }
    }
    public class Roo
    {
        public bool error { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public Data Data { get; set; }
    }
    public class Data
    {
        public int recovered { get; set; }
        public int deaths { get; set; }
        public int confirmed { get; set; }
        public DateTime lastChecked { get; set; }
        public DateTime lastReported { get; set; }
        public string location { get; set; }
    }
}