using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class SemainesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public SemainesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int user_auth_id = 8;
        int admin_auth_id = 2;

        // GET: Afficher les semaines dans un compte user
        [HttpGet]
        public async Task<IActionResult> Semaines()
        {
            Utilisateurs user = new Utilisateurs();
            List<Semaine> semaines = new List<Semaine>();
            List<SemaineProjet> semaineProjets = new List<SemaineProjet>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responseSem = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetWeeksForSpecUser/" + user_auth_id);
                if (responseSem.IsSuccessStatusCode)
                {
                    string dataSem = await responseSem.Content.ReadAsStringAsync();
                    semaines = JsonConvert.DeserializeObject<List<Semaine>>(dataSem);
                }
                if(user.Admin != "NO")
                {
                    return View("Forbidden");
                }
                var viewModel = new SemainesViewModel
                {
                    Semaines = semaines,
                    Semaine = new() { },
                    S_Projets = semaineProjets,
                    S_Projet = new() { },
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
            List<Projet> projets = new List<Projet>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
            }
            if (user.Admin != "NO")
            {
                return View("Forbidden");
            }
            HttpResponseMessage responseProjets = await _client.GetAsync(_client.BaseAddress + "/Project");
            string dataProjects = await responseProjets.Content.ReadAsStringAsync();
            projets = JsonConvert.DeserializeObject<List<Projet>>(dataProjects);
            var svm = new SemainesViewModel
            {
                Semaines = new() { },
                Semaine = new() { },
                S_Projets = new() { },
                S_Projet = new() { },
                Projets = projets,
                Utilisateur = user
            };
            return View(svm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajouter(SemainesViewModel SWP)
        {
            try
            {
                dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                obj.WeekNumber = SWP.Semaine.WeekNumber;
                obj.StatusWeek = "string";
                obj.UserId = user_auth_id;
                obj.ProjectName = SWP.S_Projet.ProjectName;
                obj.Quotation = SWP.S_Projet.Quotation;
                obj.NoAppMonday = SWP.S_Projet.NoAppMonday;
                obj.NoAppTuesday = SWP.S_Projet.NoAppTuesday;
                obj.NoAppWednesday = SWP.S_Projet.NoAppWednesday;
                obj.NoAppThursday = SWP.S_Projet.NoAppThursday;
                obj.NoAppFriday = SWP.S_Projet.NoAppFriday;
                obj.NoAppSaturday = SWP.S_Projet.NoAppSaturday;
                obj.NoAppSunday = SWP.S_Projet.NoAppSunday;
                obj.MondayStatus = "string";
                obj.TuesdayStaus = "string";
                obj.WednesdayStatus = "string";
                obj.ThursdayStatus = "string";
                obj.FridayStatus = "string";
                obj.SaturdayStatus = "string";
                obj.SundayStatus = "string";
                obj.AppMonday = 0;
                obj.AppTuesday = 0;
                obj.AppWednesday = 0;
                obj.AppThursday = 0;
                obj.AppFriday = 0;
                obj.AppSaturday = 0;
                obj.AppSunday = 0;
                obj.Bonus = 0;
                obj.TotalApp = 0;
                obj.TotalNoApp = 0;
                obj.UserId = user_auth_id;
                obj.WeekId = 0;



                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/UserWeek", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La semaine est crée avec succés";
                    TempData["action"] = "Ajouter_Sem";
                    return RedirectToAction("Semaines");
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

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                Semaine semaine = new Semaine();

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetById/" + user_auth_id + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await response.Content.ReadAsStringAsync();
                    semaine = JsonConvert.DeserializeObject<Semaine>(data);
                }
                if (user.Admin != "NO")
                {
                    return View("Forbidden");
                }
                SemainesViewModel pvm = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = semaine,
                    S_Projets = new() { },
                    S_Projet = new() { },
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
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/Delete/{user_auth_id}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La semaine et les projets travaillés sont supprimées avec succés.";
                    TempData["action"] = "Supprimer_Sem";
                    return RedirectToAction("Semaines");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        /* -------- FOR ADMIN or CDP -------- */
        [HttpGet]
        public async Task<IActionResult> SemainesUser(int id_user)
        {
            Utilisateurs user = new Utilisateurs();
            Utilisateurs utilis = new Utilisateurs();
            List<Semaine> semaines = new List<Semaine>();
            List<SemaineProjet> semaineProjets = new List<SemaineProjet>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responseSem = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetWeeksForSpecUser/" + id_user);
                if (responseSem.IsSuccessStatusCode)
                {
                    string dataSem = await responseSem.Content.ReadAsStringAsync();
                    semaines = JsonConvert.DeserializeObject<List<Semaine>>(dataSem);
                }
                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }

                HttpResponseMessage response_us = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + id_user);
                if (response_us.IsSuccessStatusCode)
                {
                    string data_us = await response_us.Content.ReadAsStringAsync();
                    utilis = JsonConvert.DeserializeObject<Utilisateurs>(data_us);
                    var viewModel = new SemainesViewModel
                    {
                        Semaines = semaines,
                        Semaine = new() { },
                        S_Projets = semaineProjets,
                        S_Projet = new() { },
                        Utilisateur = user,
                        User = utilis
                    };

                    return View(viewModel);
                }
                return View("Forbidden");
            }
            return View();
        }
    }
}
