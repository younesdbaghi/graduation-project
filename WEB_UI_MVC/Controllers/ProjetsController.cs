using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class ProjetsController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public ProjetsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int admin_auth_id = 2;

        [HttpGet]
        public async Task<IActionResult> Projets()
        {
            Utilisateurs user = new Utilisateurs();
            List<Projet> projets = new List<Projet>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responseProj = await _client.GetAsync(_client.BaseAddress + "/Project");
                if (responseProj.IsSuccessStatusCode)
                {
                    string dataProj = await responseProj.Content.ReadAsStringAsync();
                    projets = JsonConvert.DeserializeObject<List<Projet>>(dataProj);
                }
                var viewModel = new ProjetsViewModel
                {
                    Projets = projets,
                    Projet = new() { },
                    Utilisateur = user
                };

                return View(viewModel);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Ajouter()
        {
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
            }
            var viewModel = new ProjetsViewModel
            {
                Projets = new() { },
                Projet = new() { },
                Utilisateur = user
            };
            if (user.Admin == "YES" || user.Admin == "CDP")
            {
                return View(viewModel);
            }
            return View("Forbidden");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajouter(ProjetsViewModel PVM)
        {
            try
            {
                dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                obj.ProjectName = PVM.Projet.ProjectName;
                obj.HeuresTotal = PVM.Projet.HeuresTotal;


                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Project", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Le projet est crée avec succés";
                    TempData["action"] = "Ajouter_Projet";
                    return RedirectToAction("Projets");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Modifier(int id)
        {
            try
            {
                Utilisateurs user = new Utilisateurs();

                HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
                if (responseUser.IsSuccessStatusCode)
                {
                    Projet projet = new Projet();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Project/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                        string data = await response.Content.ReadAsStringAsync();
                        projet = JsonConvert.DeserializeObject<Projet>(data);
                    }
                    ProjetsViewModel pvm = new ProjetsViewModel()
                    {
                        Projets = new() { },
                        Projet = projet,
                        Utilisateur = user
                    };
                    if (user.Admin == "YES" || user.Admin == "CDP")
                    {
                        return View(pvm);
                    }
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(ProjetsViewModel PVM)
        {
            try
            {
                dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                obj.Id = PVM.Projet.Id;
                obj.ProjectName = PVM.Projet.ProjectName;
                obj.HeuresTotal = PVM.Projet.HeuresTotal;

                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/Project/" + PVM.Projet.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Le projet est modifiée avec succés";
                    TempData["action"] = "Modifier_Projet";
                    return RedirectToAction("Projets");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Supprimer(int id)
        {
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                Projet projet = new Projet();

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Project/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await response.Content.ReadAsStringAsync();
                    projet = JsonConvert.DeserializeObject<Projet>(data);
                }
                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }
                ProjetsViewModel pvm = new ProjetsViewModel()
                {
                    Projets = new() { },
                    Projet = projet,
                    Utilisateur = user
                };
                return View(pvm);
            }
            return View();
        }


        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmerSuppression(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Project/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Le projet est supprimé avec succés";
                    TempData["action"] = "Supprimer_Projet";
                    return RedirectToAction("Projets");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
