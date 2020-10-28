using MVCDemo.Data;
using MVCDemo.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private MVCDemoContext db = new MVCDemoContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PriceOil()
        {
            var viewModel = new Oil();
            var client = new RestClient("https://gas-price.p.rapidapi.com/europeanCountries");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "gas-price.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4b0f134ed5mshcc390618454924fp13e12ajsn613f77890a43");
            IRestResponse response = client.Execute(request);
            viewModel.Root = JsonConvert.DeserializeObject<Root>(response.Content);
           
            return View(viewModel.Root.results);
        }

        public async System.Threading.Tasks.Task<ActionResult> Covid(string country)
        {
            var viewModel = new Covid()
            {
                Country = country
            };
            string result = "";
            if (country != null && country != "")
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://rapidapi.p.rapidapi.com/v1/total?country={country}"),
                    Headers =
    {
        { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
        { "x-rapidapi-key", "4b0f134ed5mshcc390618454924fp13e12ajsn613f77890a43" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    result = body;
                }

                viewModel.Roo = JsonConvert.DeserializeObject<Roo>(result);
            }


            return View(viewModel);
        }

    }
}