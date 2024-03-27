using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.Controllers
{
    public class AccueilController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public AccueilController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int user_auth_id = 8;

        [HttpGet]
        public IActionResult Index()
        {
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<Utilisateurs>(data);
                return View(user);
            }
            return View("Forbidden");
        }
    }
}
