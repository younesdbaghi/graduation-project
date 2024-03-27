using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.ViewModels
{
    public class SemainesViewModel
    {
        public List<Semaine> Semaines { get; set; }
        public Semaine Semaine { get; set; }
        public List<SemaineProjet> S_Projets { get; set; }
        public SemaineProjet S_Projet { get; set; }
        public Utilisateurs Utilisateur { get; set; }
        public Utilisateurs User { get; set; }
        public int TotalSaisis { get; set; }
        public int TotalApprouvees { get; set; }
        public int TotalNonApprouvees { get; set; }
        public int Bonus { get; set; }
        public List<Projet> Projets { get; set; }
        public List<ProjetQuotations> ProjetQuotations { get; set; }
    }
}
