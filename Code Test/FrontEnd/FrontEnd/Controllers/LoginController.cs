using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        public string ErrorMessage { get; set; } = string.Empty;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiURL = "https://localhost:7202/";

        public LoginController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginPage()
        {
            return View();
        }

        public async Task<IActionResult> OnPost(UserModel userModel)
        {
            string apiUrl = $"{_apiURL}User/login";
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(userModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewPage", "Bpkb");
            }
            else
            {
                userModel.ErrorMessage = "User not found or invalid password..";
                return RedirectToAction("LoginPage", "Login");
            }

        }

    }
}
