using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class SemaineProjetsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public SemaineProjetsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int user_auth_id = 8;
        int admin_auth_id = 2;

        [HttpGet]
        public async Task<IActionResult> SemaineProjets(int id)
        {
            Utilisateurs user = new Utilisateurs();
            List<SemaineProjet> semaineProjets = new List<SemaineProjet>();
            Semaine week = new Semaine();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                HttpResponseMessage responseProjets = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetAllProjectsForThisWeek/" + user_auth_id + "/" + id);

                if (responseProjets.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await responseProjets.Content.ReadAsStringAsync();
                    semaineProjets = JsonConvert.DeserializeObject<List<SemaineProjet>>(data);
                }
                HttpResponseMessage response_week = await _client.GetAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/GetById/{user_auth_id}/{id}");
                if (response_week.IsSuccessStatusCode)
                {
                    string data_week = await response_week.Content.ReadAsStringAsync();
                    week = JsonConvert.DeserializeObject<Semaine>(data_week);
                }
                if (user.Admin != "NO")
                {
                    return View("Forbidden");
                }
                int totalH = 0;
                int totalHA = 0;
                int totalHNA = 0;
                int Bonus = 0;

                foreach (var item in semaineProjets)
                {
                    // Calcul du total des heures approuvées
                    totalHA += item.AppMonday + item.AppTuesday + item.AppWednesday +
                                              item.AppThursday + item.AppFriday + item.AppSaturday +
                                              item.AppSunday;

                    // Calcul du total des heures non approuvées
                    totalHNA += item.NoAppMonday + item.NoAppTuesday + item.NoAppWednesday +
                                              item.NoAppThursday + item.NoAppFriday + item.NoAppSaturday +
                                              item.NoAppSunday;
                }
                totalH = totalHA + totalHNA;
                Bonus = totalH - 40;
                if(!(Bonus > 0))
                {
                    Bonus = 0;
                }

                SemainesViewModel svm = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = week,
                    TotalSaisis = totalH,
                    TotalApprouvees = totalHA,
                    TotalNonApprouvees = totalHNA,
                    Bonus = Bonus,
                    S_Projets = semaineProjets,
                    S_Projet = new() { },
                    Utilisateur = user
                };
                return View(svm);
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                SemaineProjet SP = new SemaineProjet();
                Semaine week = new Semaine();


                HttpResponseMessage responseSP = await _client.GetAsync(_client.BaseAddress + "/UserWeekProject/" + id);

                if (responseSP.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await responseSP.Content.ReadAsStringAsync();
                    SP = JsonConvert.DeserializeObject<SemaineProjet>(data);
                }
                HttpResponseMessage response_week = await _client.GetAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/GetById/{SP.UserId}/{SP.WeekId}");
                if (response_week.IsSuccessStatusCode)
                {
                    string data_week = await response_week.Content.ReadAsStringAsync();
                    week = JsonConvert.DeserializeObject<Semaine>(data_week);
                }
                if (user.Admin != "NO")
                {
                    return View("Forbidden");
                }
                SemainesViewModel semainesViewModel = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = week,
                    S_Projets = new() { },
                    S_Projet = SP,
                    Utilisateur = user,
                    Projets = new() { },
                    ProjetQuotations = new() { }
                };
                return View(semainesViewModel);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Ajouter(int id)
        {
            Utilisateurs user = new Utilisateurs();
            Semaine semaine = new Semaine();
            List<Projet> projets = new List<Projet>();
            List<ProjetQuotations> projetQuotations = new List<ProjetQuotations>();


            semaine.Id = id;
            semaine.UserId = user_auth_id;

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
                Semaine = semaine,
                S_Projets = new() { },
                S_Projet = new() { },
                Utilisateur = user,
                Projets = projets,
                ProjetQuotations = new() { }
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
                obj.UserId = SWP.Semaine.UserId;
                obj.WeekId = SWP.Semaine.Id;



                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/UserWeekProject/api/UserWeekProject/Create/"+SWP.Semaine.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Les nouvelles heures sont ajoutées avec succés";
                    TempData["action"] = "Ajouter_Sem_Proj";
                    return RedirectToAction("SemaineProjets", "SemaineProjets", new { id = SWP.Semaine.Id });
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }



        // GET: SemaineProjetsController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Modifier(int id)
        {
            SemaineProjet SP = new SemaineProjet();
            Utilisateurs user = new Utilisateurs();
            Semaine week = new Semaine();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/UserWeekProject/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await response.Content.ReadAsStringAsync();
                    SP = JsonConvert.DeserializeObject<SemaineProjet>(data);

                }
                HttpResponseMessage response_week = await _client.GetAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/GetById/{SP.UserId}/{SP.WeekId}");
                if (response_week.IsSuccessStatusCode)
                {
                    string data_week = await response_week.Content.ReadAsStringAsync();
                    week = JsonConvert.DeserializeObject<Semaine>(data_week);
                }
                if (user.Admin != "NO")
                {
                    return View("Forbidden");
                }

                List<Projet> projets = new List<Projet>();
                HttpResponseMessage responseProjets = await _client.GetAsync(_client.BaseAddress + "/Project");
                string dataProjects = await responseProjets.Content.ReadAsStringAsync();
                projets = JsonConvert.DeserializeObject<List<Projet>>(dataProjects);
                SemainesViewModel semainesViewModel = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = week,
                    S_Projets = new() { },
                    S_Projet = SP,
                    Utilisateur = user,
                    Projets = projets,
                    ProjetQuotations = new() { }
                };

                return View(semainesViewModel);
            }
            return View();
        }



        // POST: SemaineProjetsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(SemainesViewModel SWP)
        {
            try
            {
                dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                obj.Id = SWP.S_Projet.Id;
                obj.ProjectName = SWP.S_Projet.ProjectName;
                obj.Quotation = SWP.S_Projet.Quotation;
                obj.NoAppMonday = SWP.S_Projet.NoAppMonday;
                obj.NoAppTuesday = SWP.S_Projet.NoAppTuesday;
                obj.NoAppWednesday = SWP.S_Projet.NoAppWednesday;
                obj.NoAppThursday = SWP.S_Projet.NoAppThursday;
                obj.NoAppFriday = SWP.S_Projet.NoAppFriday;
                obj.NoAppSaturday = SWP.S_Projet.NoAppSaturday;
                obj.NoAppSunday = SWP.S_Projet.NoAppSunday;

                obj.MondayStatus = SWP.S_Projet.MondayStatus;
                obj.TuesdayStaus = SWP.S_Projet.TuesdayStaus;
                obj.WednesdayStatus = SWP.S_Projet.WednesdayStatus;
                obj.ThursdayStatus = SWP.S_Projet.ThursdayStatus;
                obj.FridayStatus = SWP.S_Projet.FridayStatus;
                obj.SaturdayStatus = SWP.S_Projet.SaturdayStatus;                                                                                           
                obj.SundayStatus = SWP.S_Projet.SundayStatus;

                obj.AppMonday = SWP.S_Projet.AppMonday;
                obj.AppTuesday = SWP.S_Projet.AppTuesday;
                obj.AppWednesday = SWP.S_Projet.AppWednesday;
                obj.AppThursday = SWP.S_Projet.AppThursday;
                obj.AppFriday = SWP.S_Projet.AppFriday;
                obj.AppSaturday = SWP.S_Projet.AppSaturday;
                obj.AppSunday = SWP.S_Projet.AppSunday;
                obj.Bonus = SWP.S_Projet.Bonus;
                obj.TotalApp = SWP.S_Projet.TotalApp;
                obj.TotalNoApp = SWP.S_Projet.TotalNoApp;
                obj.UserId = SWP.S_Projet.UserId;
                obj.WeekId = SWP.S_Projet.WeekId;
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/UserWeekProject/" + SWP.S_Projet.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Les heures sont modifiées avec succés";
                    TempData["action"] = "Modifier_Sem_Proj";
                    return RedirectToAction("SemaineProjets", "SemaineProjets", new { id = SWP.S_Projet.WeekId });
                }
                return View("Forbidden");
            }
            catch
            {
                return View();
            }
        }

        // GET: SemaineProjetsController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Supprimer(int id, int id_week)
        {
            Utilisateurs user = new Utilisateurs();
            Semaine week = new Semaine();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                SemaineProjet SP = new SemaineProjet();

                HttpResponseMessage responseSP = await _client.GetAsync(_client.BaseAddress + "/UserWeekProject/" + id);

                if (responseSP.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await responseSP.Content.ReadAsStringAsync();
                    SP = JsonConvert.DeserializeObject<SemaineProjet>(data);
                }
                HttpResponseMessage response_week = await _client.GetAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/GetById/{SP.UserId}/{id_week}");
                if (response_week.IsSuccessStatusCode)
                {
                    string data_week = await response_week.Content.ReadAsStringAsync();
                    week = JsonConvert.DeserializeObject<Semaine>(data_week);
                }
                if (user.Admin != "NO")
                {
                    return View("Forbidden");
                }
                SemainesViewModel semainesViewModel = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = week,
                    S_Projets = new() { },
                    S_Projet = SP,
                    Utilisateur = user,
                    Projets = new() { },
                    ProjetQuotations = new() { }
                };

                return View(semainesViewModel);
            }
            return View();
        }


        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmerSuppression(int id, int id_week)
        {
            try
            {

                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/UserWeekProject/" + user_auth_id + "/" + id_week + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Le projet est supprimée avec succés";
                    TempData["action"] = "Supprimer_Sem_Proj";
                    HttpResponseMessage responseProjets = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetAllProjectsForThisWeek/" + user_auth_id + "/" + id_week);

                    if (responseProjets.IsSuccessStatusCode)
                    {
                        var semaineProjets = new List<SemaineProjet>();
                        string data = await responseProjets.Content.ReadAsStringAsync();
                        semaineProjets = JsonConvert.DeserializeObject<List<SemaineProjet>>(data);
                        if (semaineProjets.Count == 0)
                        {
                            return RedirectToAction("Semaines","Semaines");
                        }
                    }
                    return RedirectToAction("SemaineProjets", "SemaineProjets", new { id = id_week });
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        /* ------- FOR ADMIN or CDP ------- */
        [HttpGet]
        public async Task<IActionResult> SemaineProjetsUser(int id, int id_user)
        {
            Utilisateurs user = new Utilisateurs();
            Utilisateurs utilis = new Utilisateurs();
            List<SemaineProjet> semaineProjets = new List<SemaineProjet>();
            Semaine week = new Semaine();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                HttpResponseMessage responseProjets = await _client.GetAsync(_client.BaseAddress + "/UserWeek/api/UserWeek/GetAllProjectsForThisWeek/" + id_user + "/" + id);

                if (responseProjets.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await responseProjets.Content.ReadAsStringAsync();
                    semaineProjets = JsonConvert.DeserializeObject<List<SemaineProjet>>(data);
                }
                HttpResponseMessage response_week = await _client.GetAsync(_client.BaseAddress + $"/UserWeek/api/UserWeek/GetById/{id_user}/{id}");
                if (response_week.IsSuccessStatusCode)
                {
                    string data_week = await response_week.Content.ReadAsStringAsync();
                    week = JsonConvert.DeserializeObject<Semaine>(data_week);
                }
                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }
                int totalH = 0;
                int totalHA = 0;
                int totalHNA = 0;
                int Bonus = 0;

                foreach (var item in semaineProjets)
                {
                    // Calcul du total des heures approuvées
                    totalHA += item.AppMonday + item.AppTuesday + item.AppWednesday +
                                              item.AppThursday + item.AppFriday + item.AppSaturday +
                                              item.AppSunday;

                    // Calcul du total des heures non approuvées
                    totalHNA += item.NoAppMonday + item.NoAppTuesday + item.NoAppWednesday +
                                              item.NoAppThursday + item.NoAppFriday + item.NoAppSaturday +
                                              item.NoAppSunday;
                }
                totalH = totalHA + totalHNA;
                Bonus = totalH - 40;
                if (!(Bonus > 0))
                {
                    Bonus = 0;
                }


                utilis.Id = id_user;
                SemainesViewModel svm = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = week,
                    TotalSaisis = totalH,
                    TotalApprouvees = totalHA,
                    TotalNonApprouvees = totalHNA,
                    Bonus = Bonus,
                    S_Projets = semaineProjets,
                    S_Projet = new() { },
                    Utilisateur = user,
                    User = utilis
                };
                return View(svm);
            }
            return View();
        }

        
        // GET: SemaineProjetsController/Delete/5
        [HttpGet]
        public async Task<IActionResult> DetailsSemaineUser(int id)
        {
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                SemaineProjet SP = new SemaineProjet();

                HttpResponseMessage responseSP = await _client.GetAsync(_client.BaseAddress + "/UserWeekProject/" + id);

                if (responseSP.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                    string data = await responseSP.Content.ReadAsStringAsync();
                    SP = JsonConvert.DeserializeObject<SemaineProjet>(data);
                }
                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }
                SemainesViewModel svm = new SemainesViewModel()
                {
                    Semaines = new() { },
                    Semaine = new() { },
                    TotalSaisis = new() { },
                    TotalApprouvees = new() { },
                    TotalNonApprouvees = new() { },
                    Bonus = new() { },
                    S_Projets = new() { },
                    S_Projet = SP,
                    Utilisateur = user
                };
                return View(svm);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValiderSemaineUser(int id)
        {
            try
            {
                HttpResponseMessage responseProjet = await _client.GetAsync($"{_client.BaseAddress}/UserWeekProject/{id}");

                if (responseProjet.IsSuccessStatusCode)
                {
                    string data = await responseProjet.Content.ReadAsStringAsync();
                    var semaineProjet = JsonConvert.DeserializeObject<SemaineProjet>(data);

                    dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                    obj.Id = semaineProjet.Id;
                    obj.ProjectName = semaineProjet.ProjectName;
                    obj.Quotation = semaineProjet.Quotation;
                    obj.NoAppMonday = semaineProjet.AppMonday;
                    obj.NoAppTuesday = semaineProjet.AppTuesday;
                    obj.NoAppWednesday = semaineProjet.AppWednesday;
                    obj.NoAppThursday = semaineProjet.AppThursday;
                    obj.NoAppFriday = semaineProjet.AppFriday;
                    obj.NoAppSaturday = semaineProjet.AppSaturday;
                    obj.NoAppSunday = semaineProjet.AppSunday;

                    obj.MondayStatus = "Approuvée";
                    obj.TuesdayStaus = "Approuvée";
                    obj.WednesdayStatus = "Approuvée";
                    obj.ThursdayStatus = "Approuvée";
                    obj.FridayStatus = "Approuvée";
                    obj.SaturdayStatus = "Approuvée";
                    obj.SundayStatus = "Approuvée";

                    obj.AppMonday = semaineProjet.NoAppMonday;
                    obj.AppTuesday = semaineProjet.NoAppTuesday;
                    obj.AppWednesday = semaineProjet.NoAppWednesday;
                    obj.AppThursday = semaineProjet.NoAppThursday;
                    obj.AppFriday = semaineProjet.NoAppFriday;
                    obj.AppSaturday = semaineProjet.NoAppSaturday;
                    obj.AppSunday = semaineProjet.NoAppSunday;
                    obj.Bonus = semaineProjet.Bonus;
                    obj.TotalApp = semaineProjet.TotalApp;
                    obj.TotalNoApp = semaineProjet.TotalNoApp;
                    obj.UserId = semaineProjet.UserId;
                    obj.WeekId = semaineProjet.WeekId;

                    string dataObj = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(dataObj, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/UserWeekProject/" + id, content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Les heures travaillés sur ce projet sont validées avec succès";
                        TempData["action"] = "Valider_Sem_Proj";
                        return RedirectToAction("SemaineProjetsUser", "SemaineProjets", new { id = semaineProjet.WeekId, id_user = semaineProjet.UserId });
                    }
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                // Gérer l'exception (log, message d'erreur, etc.)
                ViewBag.ErrorMessage = "Une erreur s'est produite lors de la validation des heures.";
                return View("Error");
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvaliderSemaineUser(int id)
        {
            try
            {
                HttpResponseMessage responseProjet = await _client.GetAsync($"{_client.BaseAddress}/UserWeekProject/{id}");

                if (responseProjet.IsSuccessStatusCode)
                {
                    string data = await responseProjet.Content.ReadAsStringAsync();
                    var semaineProjet = JsonConvert.DeserializeObject<SemaineProjet>(data);

                    dynamic obj = new System.Dynamic.ExpandoObject(); // Crée un nouvel objet dynamique vide
                    obj.Id = semaineProjet.Id;
                    obj.ProjectName = semaineProjet.ProjectName;
                    obj.Quotation = semaineProjet.Quotation;
                    obj.NoAppMonday = semaineProjet.AppMonday;
                    obj.NoAppTuesday = semaineProjet.AppTuesday;
                    obj.NoAppWednesday = semaineProjet.AppWednesday;
                    obj.NoAppThursday = semaineProjet.AppThursday;
                    obj.NoAppFriday = semaineProjet.AppFriday;
                    obj.NoAppSaturday = semaineProjet.AppSaturday;
                    obj.NoAppSunday = semaineProjet.AppSunday;

                    obj.MondayStatus = "Non Approuvée";
                    obj.TuesdayStaus = "Non Approuvée";
                    obj.WednesdayStatus = "Non Approuvée";
                    obj.ThursdayStatus = "Non Approuvée";
                    obj.FridayStatus = "Non Approuvée";
                    obj.SaturdayStatus = "Non Approuvée";
                    obj.SundayStatus = "Non Approuvée";

                    obj.AppMonday = semaineProjet.NoAppMonday;
                    obj.AppTuesday = semaineProjet.NoAppTuesday;
                    obj.AppWednesday = semaineProjet.NoAppWednesday;
                    obj.AppThursday = semaineProjet.NoAppThursday;
                    obj.AppFriday = semaineProjet.NoAppFriday;
                    obj.AppSaturday = semaineProjet.NoAppSaturday;
                    obj.AppSunday = semaineProjet.NoAppSunday;
                    obj.Bonus = semaineProjet.Bonus;
                    obj.TotalApp = semaineProjet.TotalApp;
                    obj.TotalNoApp = semaineProjet.TotalNoApp;
                    obj.UserId = semaineProjet.UserId;
                    obj.WeekId = semaineProjet.WeekId;

                    string dataObj = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(dataObj, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/UserWeekProject/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Les heures travaillés sur ce projet ne sont pas validées avec succès";
                        TempData["action"] = "Invalider_Sem_Proj";
                        return RedirectToAction("SemaineProjetsUser", "SemaineProjets", new { id = semaineProjet.WeekId, id_user = semaineProjet.UserId });
                    }
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                // Gérer l'exception (log, message d'erreur, etc.)
                ViewBag.ErrorMessage = "Une erreur s'est produite lors de la validation des heures.";
                return View("Error");
            }
        }
    }
}
