using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_UI_MVC.Auth;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class AuthController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public AuthController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        [HttpGet]
        public async Task<IActionResult> Se_Connecter()
        {
            if (Authentication.Connected)
            {
                return RedirectToAction("Index","Accueil");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login_Traitement(UserLogin credentials)
        {
            List<Utilisateurs> utilisateurs = new List<Utilisateurs>();
            HttpResponseMessage responseUsers = await _client.GetAsync(_client.BaseAddress + "/User");
            if (responseUsers.IsSuccessStatusCode)
            {
                string dataUsers = await responseUsers.Content.ReadAsStringAsync();
                utilisateurs = JsonConvert.DeserializeObject<List<Utilisateurs>>(dataUsers);

                foreach(var user in utilisateurs)
                {
                    if((user.Username == credentials.login || user.Email == credentials.login) && (user.Password == credentials.password))
                    {
                        Authentication.Connected = true;
                        Authentication.Connected_Id = user.Id;
                        return RedirectToAction("Index", "Accueil");
                    }
                }
                TempData["action"] = "Invalid_Cred";
                return RedirectToAction("Se_Connecter");
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Logout_Traitement()
        {
            Authentication.Connected = false;
            Authentication.Connected_Id = 0;
            return RedirectToAction("Se_Connecter");
        }
    }
}
