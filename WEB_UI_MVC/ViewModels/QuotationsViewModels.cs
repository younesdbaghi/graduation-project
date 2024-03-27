using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.ViewModels
{
    public class QuotationsViewModels
    {
        public Utilisateurs Utilisateur { get; set; }
        public List<Projet> Projets { get; set; }
        public Projet Projet { get; set; }
    }
}
