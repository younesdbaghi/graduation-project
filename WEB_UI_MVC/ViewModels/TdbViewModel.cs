using WEB_UI_MVC.Models;
using WEB_UI_MVC.Objects;

namespace WEB_UI_MVC.ViewModels
{
    public class TdbViewModel
    {
        public List<Utilisateurs> Utilisateurs { get; set; }
        public Utilisateurs User { get; set; }
        public List<Projet> Projets { get; set; }
        public Projet Projet { get; set; }
        public List<SemaineProjet> SemProjets { get; set; }
        public double Progress { get; set; }
        public List<UserProjectObject> UserProjectObject { get; set; }
        public int HeuresTravaillees {  get; set; }
    }
}
