using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FrontEnd.Controllers
{
    public class BpkbController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiURL = "https://localhost:7202/";

        public BpkbController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #region Load Page
        public async Task<IActionResult> AddPage()
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

        public async Task<IActionResult> ViewPage()
        {
            string apiUrl = $"{_apiURL}TrBpkb/GetListBpkb"; // Replace with your actual API endpoint
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BpkbModel>>(data);
                return View(result);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to load BPKB.." });
            }
        }

        public async Task<IActionResult> EditPage(string id)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(id))
            {
                return View("Error", new ErrorViewModel { RequestId = "Agreement number is required.." });
            }

            string apiUrl = $"{_apiURL}TrBpkb/GetBpkbByAgreementNumber?agreementNumber={id}";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var bpkbData = JsonConvert.DeserializeObject<BpkbModel>(data);
                var result = new EditModel
                {
                    AgreementNumber = bpkbData.AgreementNumber,
                    BpkbDate = bpkbData.BpkbDate,
                    BpkbDateIn = bpkbData.BpkbDateIn,
                    BpkbNo = bpkbData.BpkbNo,
                    BranchId = bpkbData.BranchId,
                    LocationId = bpkbData.LocationId,
                    PoliceNo = bpkbData.PoliceNo,
                    FakturDate = bpkbData.FakturDate,
                    FakturNo = bpkbData.FakturNo,
                    locations = await GetLocationsAsync()
                };
                return View(result);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = $"Failed to load BPKB for agreement number: {id}" });
            }
        }

        public async Task<List<LocationModel>> GetLocationsAsync()
        {
            string apiUrl = $"{_apiURL}Location";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LocationModel>>(data);
            }
            else
            {
                throw new Exception("Failed to load locations.");
            }
        }


        public async Task<IActionResult> DeletePage(string id)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(id))
            {
                return View("Error", new ErrorViewModel { RequestId = "Agreement number is required.." });
            }

            string apiUrl = $"{_apiURL}TrBpkb/GetBpkbByAgreementNumber?agreementNumber={id}";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var bpkbData = JsonConvert.DeserializeObject<BpkbModel>(data);
                var result = new DeleteModel
                {
                    AgreementNumber = bpkbData.AgreementNumber,
                    BpkbDate = bpkbData.BpkbDate,
                    BpkbDateIn = bpkbData.BpkbDateIn,
                    BpkbNo = bpkbData.BpkbNo,
                    BranchId = bpkbData.BranchId,
                    LocationId = bpkbData.LocationId,
                    PoliceNo = bpkbData.PoliceNo,
                    FakturDate = bpkbData.FakturDate,
                    FakturNo = bpkbData.FakturNo,
                    locations = await GetLocationsAsync()
                };
                return View(result);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = $"Failed to load BPKB for agreement number: {id}" });
            }
        }
        #endregion

        #region Action button
        public async Task<IActionResult> Save(BpkbModel bpkbModel)
        {
            string apiUrl = $"{_apiURL}TrBpkb/AddBpkb";
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(bpkbModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewPage", "Bpkb");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Error saving data.." });
            }
        }

        public async Task<IActionResult> Update(BpkbModel bpkbModel)
        {
            string apiUrl = $"{_apiURL}TrBpkb/UpdateBpkb";
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(bpkbModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewPage", "Bpkb");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Error update data.." });
            }
        }

        public async Task<IActionResult> Delete(BpkbModel bpkbModel)
        {
            if (string.IsNullOrWhiteSpace(bpkbModel.AgreementNumber))
            {
                return View("Error", new ErrorViewModel { RequestId = "Agreement number is required.." });
            }

            string apiUrl = $"{_apiURL}TrBpkb/DeleteBpkb?agreementNumber={bpkbModel.AgreementNumber}";
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewPage", "Bpkb");
            }
            else
            {
                throw new Exception("Error delete data..");
            }
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
