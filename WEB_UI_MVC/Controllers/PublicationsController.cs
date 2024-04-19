using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;
using WEB_UI_MVC.Auth;


namespace WEB_UI_MVC.Controllers
{
    public class PublicationsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public PublicationsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int user_auth_id = Authentication.Connected_Id; /* 8 */

        // GET: PublicationsController
        [HttpGet]
        public async Task<IActionResult> Publications()
        {
            if (!Authentication.Connected)
            {
                return RedirectToAction("Se_Connecter", "Auth");
            }
            Utilisateurs user = new Utilisateurs();
            List<Publication> publications = new List<Publication>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                HttpResponseMessage responsePub = await _client.GetAsync(_client.BaseAddress + "/Publication");
                if (responsePub.IsSuccessStatusCode)
                {
                    string dataPub = await responsePub.Content.ReadAsStringAsync();
                    publications = JsonConvert.DeserializeObject<List<Publication>>(dataPub);
                }
                var viewModel = new PublicationsViewModel
                {
                    Publications = publications,
                    Publication = new() { },
                    Utilisateur = user
                };

                return View(viewModel);
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Ajouter()
        {
            if (!Authentication.Connected)
            {
                return RedirectToAction("Se_Connecter", "Auth");
            }
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                if (user.Admin != "YES" && user.Admin != "CDP")
                {
                    return View("Forbidden");
                }
            }
            var viewModel = new PublicationsViewModel
            {
                Publications = new() { },
                Publication = new() { },
                Utilisateur = user
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajouter(Publication publication)
        {
            try
            {
                publication.Date = DateTime.Now;
                publication.Heure = publication.Date.ToString("HH:mm");
                string data = JsonConvert.SerializeObject(publication);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/publication", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La publication est crée avec succés";
                    TempData["action"] = "Ajouter";
                    return RedirectToAction("Publications");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: PublicationsController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Modifier(int id)
        {
            if (!Authentication.Connected)
            {
                return RedirectToAction("Se_Connecter", "Auth");
            }
            try
            {
                Utilisateurs user = new Utilisateurs();

                HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
                if (responseUser.IsSuccessStatusCode)
                {
                    Publication publication = new Publication();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/publication/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                        if (user.Admin != "YES" && user.Admin != "CDP")
                        {
                            return View("Forbidden");
                        }

                        string data = await response.Content.ReadAsStringAsync();
                        publication = JsonConvert.DeserializeObject<Publication>(data);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                    PublicationsViewModel pvm = new PublicationsViewModel()
                    {
                        Publications = new() { },
                        Publication = publication,
                        Utilisateur = user
                    };
                    return View(pvm);
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
        public async Task<IActionResult> Modifier(Publication publication)
        {
            try
            {
                string data = JsonConvert.SerializeObject(publication);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/publication/"+publication.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La publication est modifiée avec succés";
                    TempData["action"] = "Modifier";
                    return RedirectToAction("Publications");
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
            if (!Authentication.Connected)
            {
                return RedirectToAction("Se_Connecter", "Auth");
            }
            Utilisateurs user = new Utilisateurs();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                Publication publication = new Publication();

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/publication/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                    if (user.Admin != "YES" && user.Admin != "CDP")
                    {
                        return View("Forbidden");
                    }

                    string data = await response.Content.ReadAsStringAsync();
                    publication = JsonConvert.DeserializeObject<Publication>(data);
                }
                else
                {
                    return View("NotFound");
                }
                PublicationsViewModel pvm = new PublicationsViewModel()
                {
                    Publications = new() { },
                    Publication = publication,
                    Utilisateur = user
                };
                if (user.Admin == "YES" || user.Admin == "CDP")
                {
                    return View(pvm);
                }
            }
            return View("Forbidden");
        }


        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmerSuppression(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/Publication/api/Publication/Delete/" + id + "/" + user_auth_id);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "La publication est supprimée avec succés";
                    TempData["action"] = "Supprimer";
                    return RedirectToAction("Publications");
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
