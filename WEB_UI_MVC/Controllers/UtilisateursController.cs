using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;
using WEB_UI_MVC.Auth;


namespace WEB_UI_MVC.Controllers
{
    public class UtilisateursController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public UtilisateursController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int admin_auth_id = Authentication.Connected_Id;

        // GET: PublicationsController
        [HttpGet]
        public async Task<IActionResult> Utilisateurs()
        {
            if (!Authentication.Connected)
            {
                return RedirectToAction("Se_Connecter", "Auth");
            }
            Utilisateurs user = new Utilisateurs();
            List<Utilisateurs> utilisateurs = new List<Utilisateurs>();

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                if (user.Admin != "YES" && user.Admin != "CDP")
                {
                    return View("Forbidden");
                }

                HttpResponseMessage responseUsers = await _client.GetAsync(_client.BaseAddress + "/User");
                if (responseUsers.IsSuccessStatusCode)
                {
                    string dataUsers = await responseUsers.Content.ReadAsStringAsync();
                    utilisateurs = JsonConvert.DeserializeObject<List<Utilisateurs>>(dataUsers);
                }

                
                var viewModel = new UsersViewModel
                {
                    Users = utilisateurs,
                    User = new() { },
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

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                string dataUser = await responseUser.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                if (user.Admin != "YES")
                {
                    return View("Forbidden");
                }
            }
            var viewModel = new UsersViewModel
            {
                Users = new() { },
                User = new() { },
                Utilisateur = user
            };
            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajouter(UsersViewModel utilisateur)
        {
            try
            {
                utilisateur.User.Password = utilisateur.User.Password;
                string data = JsonConvert.SerializeObject(utilisateur.User);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/User", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "L'utilisateur est crée avec succés";
                    TempData["action"] = "Ajouter";
                    return RedirectToAction("Utilisateurs");
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
                if (!Authentication.Connected)
                {
                    return RedirectToAction("Se_Connecter", "Auth");
                }
                Utilisateurs user = new Utilisateurs();

                HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
                if (responseUser.IsSuccessStatusCode)
                {
                    Utilisateurs utilisateurForUpdate = new Utilisateurs();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                        if (user.Admin != "YES")
                        {
                            return View("Forbidden");
                        }

                        string data = await response.Content.ReadAsStringAsync();
                        utilisateurForUpdate = JsonConvert.DeserializeObject<Utilisateurs>(data);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                    var viewModel = new UsersViewModel
                    {
                        Users = new() { },
                        User = utilisateurForUpdate,
                        Utilisateur = user
                    };
                    return View(viewModel);
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(UsersViewModel utilisateur)
        {
            try
            {
                utilisateur.User.Password = utilisateur.User.Password;
                string data = JsonConvert.SerializeObject(utilisateur.User);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/User/" + utilisateur.User.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "L'utilisateur est modifié avec succés";
                    TempData["action"] = "Modifier";
                    return RedirectToAction("Utilisateurs");
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

            HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
            if (responseUser.IsSuccessStatusCode)
            {
                Utilisateurs utilisateur = new Utilisateurs();

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string dataUser = await responseUser.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                    if (user.Admin != "YES")
                    {
                        return View("Forbidden");
                    }

                    string data = await response.Content.ReadAsStringAsync();
                    utilisateur = JsonConvert.DeserializeObject<Utilisateurs>(data);
                }
                else
                {
                    return View("NotFound");
                }
                var viewModel = new UsersViewModel
                {
                    Users = new() { },
                    User = utilisateur,
                    Utilisateur = user
                };
                return View(viewModel);
            }
            return View();
        }


        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmerSuppression(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/User/" + id);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "L'utilisateur est supprimé avec succés";
                    TempData["action"] = "Supprimer";
                    return RedirectToAction("Utilisateurs");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }



        /* Modifier le mot de passe */
        [HttpGet]
        public async Task<ActionResult> Changer_Le_MDP(int id)
        {
            try
            {
                if (!Authentication.Connected)
                {
                    return RedirectToAction("Se_Connecter", "Auth");
                }
                Utilisateurs user = new Utilisateurs();

                HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + admin_auth_id);
                if (responseUser.IsSuccessStatusCode)
                {
                    Utilisateurs utilisateurForUpdate = new Utilisateurs();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);
                        if(user.Id != id)
                        {
                            return View("Forbidden");
                        }

                        string data = await response.Content.ReadAsStringAsync();
                        utilisateurForUpdate = JsonConvert.DeserializeObject<Utilisateurs>(data);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                    var viewModel = new UsersViewModel
                    {
                        Users = new() { },
                        User = utilisateurForUpdate,
                        Utilisateur = user
                    };
                    return View(viewModel);
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changer_Le_MDP(UsersViewModel utilisateur)
        {
            try
            {
                Utilisateurs utilisateurForUpdate = new Utilisateurs();

                HttpResponseMessage responseUpUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + utilisateur.User.Id);

                if (responseUpUser.IsSuccessStatusCode)
                {
                    string dataUpUser = await responseUpUser.Content.ReadAsStringAsync();
                    utilisateurForUpdate = JsonConvert.DeserializeObject<Utilisateurs>(dataUpUser);
                }

                if(utilisateurForUpdate.Id!=utilisateur.User.Id || utilisateurForUpdate.Name != utilisateur.User.Name
                    || utilisateurForUpdate.LastName != utilisateur.User.LastName || utilisateurForUpdate.LastName != utilisateur.User.LastName
                    || utilisateurForUpdate.Email != utilisateur.User.Email || utilisateurForUpdate.Username != utilisateur.User.Username || utilisateurForUpdate.Activity != utilisateur.User.Activity
                    || utilisateurForUpdate.Admin != utilisateur.User.Admin)
                {
                    return View("Forbidden");
                }

                string data = JsonConvert.SerializeObject(utilisateur.User);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/User/" + utilisateur.User.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Le mot de passe est modifié avec succés";
                    TempData["action"] = "Modifier_Password";
                    return RedirectToAction("Changer_Le_MDP", new { id = utilisateur.User.Id });
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
