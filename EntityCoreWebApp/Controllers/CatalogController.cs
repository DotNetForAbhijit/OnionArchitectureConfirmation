using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesignPatternWebApp.Controllers
{
    public class CatalogController : Controller
    {
        IConfiguration _config;
        HttpClient client;
        Uri baseAddress;
        public CatalogController(IConfiguration config)
        {
            _config = config;
            baseAddress = new Uri(config["ApiAddress"]);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> modelList = new List<ProductModel>();
            var response = client.GetAsync(client.BaseAddress + "/catalog").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
            }
            return View(modelList);
        }
    }
}
