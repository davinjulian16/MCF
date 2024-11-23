using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiURL = "https://localhost:7202/";

        public LocationController( IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<IActionResult> GetLocation()
        {
            string apiUrl = $"{_apiURL}Location"; // Replace with your actual API endpoint
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<LocationModel>>(data);
                return View(result);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to load locations.." });
            }
        }
    }
}
