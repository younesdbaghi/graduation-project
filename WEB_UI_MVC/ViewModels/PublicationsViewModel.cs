using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.ViewModels
{
    public class PublicationsViewModel
    {
        public List<Publication> Publications { get; set; }
        public Publication Publication { get; set; }
        public Utilisateurs Utilisateur { get; set; }
    }
}
