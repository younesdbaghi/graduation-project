using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.ViewModels
{
    public class ProjetsViewModel
    {
        public List<Projet> Projets { get; set; }
        public Projet Projet { get; set; }
        public List<ProjetQuotations> Pro_Quotations { get; set; }
        public Utilisateurs Utilisateur { get; set; }
        public ProjetQuotations Quotation { get; set; }
    }
}
