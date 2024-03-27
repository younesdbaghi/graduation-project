using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WEB_UI_MVC.Models
{
    public class Projet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du projet est requise.")]
        [MaxLength(100, ErrorMessage = "Le nom du projet ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Nom du projet")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Les heures totales du projet sont requises.")]
        [Range(100, 1000000, ErrorMessage = "Les heures totales du projet doivent etre entre {1} et {2} heures.")]
        [DisplayName("Heures totales du projet")]
        public int HeuresTotal { get; set; }
        public virtual List<ProjetQuotations>? ProjectQuotations { get; set; }
        public virtual List<ProjetStatistiques> ProjectStatistics { get; set; }
    }
}
