using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WEB_UI_MVC.Models
{
    public class Semaine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le numero de la semaine est requise.")]
        [MaxLength(9, ErrorMessage = "Le numero de la semaine ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("N° de la semaine")]
        public string WeekNumber { get; set; }

        [Required(ErrorMessage = "Le statut de la semaine est requise.")]
        [MaxLength(100, ErrorMessage = "Le statut de la semaine ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Statut")]
        public string StatusWeek { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual Utilisateurs User { get; set; }
        public virtual List<SemaineProjet> UserWeekProjects { get; set; }
    }
}
