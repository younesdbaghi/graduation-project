using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WEB_UI_MVC.Models
{
    public class Utilisateurs
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est requise.")]
        [MaxLength(100, ErrorMessage = "Le prénom ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Prénom")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le nom est requise.")]
        [MaxLength(100, ErrorMessage = "Le nom ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requise.")]
        [MaxLength(100, ErrorMessage = "Le nom d'utilisateur ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Nom d'utilisateur")]
        public string Username { get; set; }

        [Required(ErrorMessage = "L'e-mail est requise.")]
        [MaxLength(100, ErrorMessage = "L'e-mail ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requise.")]
        [MinLength(8, ErrorMessage = "Le Mot de passe doit avoir au minimum {1} caractères.")]
        [DisplayName("Mot de passe")]
        public string Password { get; set; }

        [Required(ErrorMessage = "L'activité est requise.")]
        [MaxLength(100, ErrorMessage = "L'activité ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Activité")]
        public string Activity { get; set; }

        [Required(ErrorMessage = "L'admin est requise.")]
        [MaxLength(3, ErrorMessage = "L'admin ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Amin")]
        public string Admin { get; set; }

        public virtual List<SemaineProjet> ProjetsTravailles { get; set; }
    }
}
