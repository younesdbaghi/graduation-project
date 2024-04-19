using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB_UI_MVC.Models;
using WEB_UI_MVC.ViewModels;
using WEB_UI_MVC.Auth;


namespace WEB_UI_MVC.Controllers
{
    public class PasswordController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:51379/api");
        private readonly HttpClient _client;

        public PasswordController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /* L'utilisateur connecté */
        int user_auth_id = Authentication.Connected_Id; /* 8 */

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

                HttpResponseMessage responseUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + user_auth_id);
                if (responseUser.IsSuccessStatusCode)
                {
                    Utilisateurs utilisateurForUpdate = new Utilisateurs();

                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + id);

                    if (response.IsSuccessStatusCode && id == user_auth_id)
                    {
                        string dataUser = await responseUser.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Utilisateurs>(dataUser);

                        string data = await response.Content.ReadAsStringAsync();
                        utilisateurForUpdate = JsonConvert.DeserializeObject<Utilisateurs>(data);
                        var viewModel = new UsersViewModel
                        {
                            Users = new() { },
                            User = utilisateurForUpdate,
                            Utilisateur = user
                        };
                        return View(viewModel);
                    }
                    return View("Forbidden");
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changer_Le_MDP(UsersViewModel utilisateur)
        {
            try
            {
                if(utilisateur.User.Id != user_auth_id)
                {
                    return View("Forbidden");
                }

                Utilisateurs utilisateurForUpdate = new Utilisateurs();
                HttpResponseMessage responseUpUser = await _client.GetAsync(_client.BaseAddress + "/User/api/User/GetById/" + utilisateur.User.Id);

                if (responseUpUser.IsSuccessStatusCode)
                {
                    string dataUpUser = await responseUpUser.Content.ReadAsStringAsync();
                    utilisateurForUpdate = JsonConvert.DeserializeObject<Utilisateurs>(dataUpUser);
                    if ((utilisateurForUpdate.Id != utilisateur.User.Id || utilisateurForUpdate.Name != utilisateur.User.Name
                    || utilisateurForUpdate.LastName != utilisateur.User.LastName || utilisateurForUpdate.Email != utilisateur.User.Email 
                    || utilisateurForUpdate.Username != utilisateur.User.Username || utilisateurForUpdate.Activity != utilisateur.User.Activity
                    || utilisateurForUpdate.Admin != utilisateur.User.Admin) || utilisateurForUpdate.Id != utilisateur.User.Id)
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
                }
                return View("Forbidden");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Error");
            }
        }
    }
}
