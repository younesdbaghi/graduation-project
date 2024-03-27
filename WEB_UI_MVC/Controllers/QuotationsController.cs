using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class QuotationsController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public QuotationsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int admin_auth_id = 2;


        [HttpGet]
        public async Task<IActionResult> Quotations(int id_project)
        {
            Utilisateurs user = new Utilisateurs();
            Projet projet = new Projet();
            ProjetQuotations Quot = new ProjetQuotations();
            Quot.ProjectId = id_project;

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responseProj = await _client.GetAsync(_client.BaseAddress + $"/Project/{id_project}");
                if (responseProj.IsSuccessStatusCode)
                {
                    string dataProj = await responseProj.Content.ReadAsStringAsync();
                    projet = JsonConvert.DeserializeObject<Projet>(dataProj);
                }
                var viewModel = new ProjetsViewModel
                {
                    Projets = new() { },
                    Projet = projet,
                    Pro_Quotations = projet.ProjectQuotations,
                    Utilisateur = user,
                    Quotation = Quot
                };

                return View(viewModel);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Ajouter(int id_project)
        {
            Utilisateurs user = new Utilisateurs();
            ProjetQuotations Quot = new ProjetQuotations();

            Quot.ProjectId = id_project;

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
                Pro_Quotations = new() { },
                Utilisateur = user,
                Quotation = Quot
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
                obj.Quotation = PVM.Quotation.Quotation;
                obj.ProjectId = PVM.Quotation.ProjectId;


                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ProjectQuotation", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La quotation est crée avec succés";
                    TempData["action"] = "Ajouter_Quot";
                    return RedirectToAction("Quotations", "Quotations", new { id_project = PVM.Quotation.ProjectId });
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
                    ProjetQuotations quotation = new ProjetQuotations();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/ProjectQuotation/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                        string data = await response.Content.ReadAsStringAsync();
                        quotation = JsonConvert.DeserializeObject<ProjetQuotations>(data);
                    }
                    ProjetsViewModel pvm = new ProjetsViewModel()
                    {
                        Projets = new() { },
                        Projet = new() { },
                        Pro_Quotations = new() { },
                        Utilisateur = user,
                        Quotation = quotation
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
                obj.Id = PVM.Quotation.Id;
                obj.Quotation = PVM.Quotation.Quotation;
                obj.ProjectId = PVM.Quotation.ProjectId;

                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/ProjectQuotation/" + PVM.Quotation.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La quotation est modifiée avec succés";
                    TempData["action"] = "Modifier_Quot";
                    return RedirectToAction("Quotations", "Quotations", new { id_project = PVM.Quotation.ProjectId });
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
                ProjetQuotations quotation = new ProjetQuotations();

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/ProjectQuotation/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await response.Content.ReadAsStringAsync();
                    quotation = JsonConvert.DeserializeObject<ProjetQuotations>(data);
                }
                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }
                ProjetsViewModel pvm = new ProjetsViewModel()
                {
                    Projets = new() { },
                    Projet = new() { },
                    Pro_Quotations = new() { },
                    Utilisateur = user,
                    Quotation = quotation
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
                HttpResponseMessage response_Proj = await _client.GetAsync(_client.BaseAddress + $"/ProjectQuotation/{id}");
                string dataProj = await response_Proj.Content.ReadAsStringAsync();
                ProjetQuotations projetQuot = new ProjetQuotations();
                projetQuot = JsonConvert.DeserializeObject<ProjetQuotations>(dataProj);

                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/ProjectQuotation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La quotation est supprimée avec succés";
                    TempData["action"] = "Supprimer_Quot";

                    return RedirectToAction("Quotations", "Quotations", new { id_project = projetQuot.ProjectId });
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
