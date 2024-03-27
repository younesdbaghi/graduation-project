using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WEB_UI_MVC.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre de la semaine est requise.")]
        [MaxLength(100, ErrorMessage = "Le titre de la semaine ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Titre de la publication")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [MinLength(20, ErrorMessage = "La description doit avoir au minimum {1} caractères.")]
        [MaxLength(700, ErrorMessage = "La description ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Heure { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public virtual Utilisateurs User {get; set;}
    }
}
