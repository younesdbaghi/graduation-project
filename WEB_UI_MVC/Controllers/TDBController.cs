using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.Objects;
using WEB_UI_MVC.ViewModels;

namespace WEB_UI_MVC.Controllers
{
    public class TDBController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public TDBController()
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

                HttpResponseMessage responseUsers = await _client.GetAsync(_client.BaseAddress + "/Project");
                if (responseUsers.IsSuccessStatusCode)
                {
                    string dataProjets = await responseUsers.Content.ReadAsStringAsync();
                    projets = JsonConvert.DeserializeObject<List<Projet>>(dataProjets);
                }

                if (user.Admin == "NO")
                {
                    return View("Forbidden");
                }
                var viewModel = new TdbViewModel
                {
                    Utilisateurs = new() { },
                    User = user,
                    Projets = projets,
                    Projet = new() { }
                };

                return View(viewModel);
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Statistiques(int id_projet, string nom_projet)
        {
            Utilisateurs user = new Utilisateurs();
            Utilisateurs us = new Utilisateurs();
            Projet projet = new Projet();
            List<SemaineProjet> projets = new List<SemaineProjet>();
            List<UserProjectObject> UPO = new List<UserProjectObject>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responseProj = await _client.GetAsync(_client.BaseAddress + "/Project/" + id_projet);
                if (responseProj.IsSuccessStatusCode)
                {
                    string dataProj = await responseProj.Content.ReadAsStringAsync();
                    projet = JsonConvert.DeserializeObject<Projet>(dataProj);

                    HttpResponseMessage responseProjects = await _client.GetAsync(_client.BaseAddress + "/UserWeekProject");
                    if (responseProjects.IsSuccessStatusCode)
                    {
                        string dataProjets = await responseProjects.Content.ReadAsStringAsync();
                        projets = JsonConvert.DeserializeObject<List<SemaineProjet>>(dataProjets);
                    }

                    if (user.Admin == "NO")
                    {
                        return View("Forbidden");
                    }

                    /* ----- ----- ----- ----- ----- ----- */
                    /* ----- CALCUL DES STATISTIQUES ----- */
                    /* ----- ----- ----- ----- ----- ----- */
                    int TH_Travaillees = 0;
                    double PROGRESS = 0;
                    foreach(var item in projets)
                    {
                        if (item.ProjectName == nom_projet)
                        {
                            TH_Travaillees += item.TotalApp;

                            HttpResponseMessage response_US = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + item.UserId);
                            if (response_US.IsSuccessStatusCode)
                            {
                                UserProjectObject upo = new UserProjectObject();

                                string dataUS = await response_US.Content.ReadAsStringAsync();
                                us = JsonConvert.DeserializeObject<Utilisateurs>(dataUS);

                                upo.Id = item.UserId;
                                upo.NomComplet = $"{us.Name.First().ToString().ToUpper()}{us.Name.Substring(1).ToLower()} {us.LastName.First().ToString().ToUpper()}{us.LastName.Substring(1).ToLower()}";
                                upo.TotalApprouved = item.TotalApp;

                                UPO.Add(upo);
                            }
                        }
                    }
                    PROGRESS = TH_Travaillees * 100 / projet.HeuresTotal;

                    var aggregatedUPO = UPO
                        .OrderByDescending(u => u.TotalApprouved)
                        .GroupBy(u => u.Id)
                        .Select(g => new UserProjectObject
                        {
                            Id = g.Key,
                            NomComplet = g.First().NomComplet,
                            TotalApprouved = g.Sum(u => u.TotalApprouved)
                        })
                        .ToList();
                    /* ----- ----- ----- ----- ----- ----- */
                    /* ----- ----- ----- ----- ----- ----- */

                    var viewModel = new TdbViewModel
                    {
                        Utilisateurs = new() { },
                        User = user,
                        Projets = new() { },
                        Projet = projet,
                        SemProjets = projets,
                        Progress = PROGRESS,
                        UserProjectObject = aggregatedUPO,
                        HeuresTravaillees = TH_Travaillees
                    };
                    return View(viewModel);
                }
            }

            return View();
        }




        // GET: TDBController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TDBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TDBController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TDBController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TDBController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TDBController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TDBController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
