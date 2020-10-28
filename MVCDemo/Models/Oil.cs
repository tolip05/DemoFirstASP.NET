using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class Oil
    {
        public Root Root { get; set; }
    }
    public class Root
    {
        public Root()
        {
            this.results = new List<Result>();
        }
        public List<Result> results { get; set; }
    }
    public class Result
    {
        public string currency { get; set; }
        public string lpg { get; set; }
        public string diesel { get; set; }
        public string gasoline { get; set; }
        public string country { get; set; }
    }
}