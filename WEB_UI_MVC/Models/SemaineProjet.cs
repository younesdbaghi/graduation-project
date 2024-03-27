using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace WEB_UI_MVC.Models
{
    public class SemaineProjet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de projet est requise.")]
        [MaxLength(100, ErrorMessage = "Le nom de projet ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Nom de projet")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "La quotation est requise.")]
        [MaxLength(100, ErrorMessage = "La quotation ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Quotation")]
        public string Quotation { get; set; }

        [Required(ErrorMessage = "Les heures de lundi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de lundi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Lundi")]
        public int NoAppMonday { get; set; }

        [Required(ErrorMessage = "Les heures de mardi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de mardi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Mardi")]
        public int NoAppTuesday { get; set; }

        [Required(ErrorMessage = "Les heures de mercredi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de mercredi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Mercredi")]
        public int NoAppWednesday { get; set; }

        [Required(ErrorMessage = "Les heures de jeudi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de jeudi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Jeudi")]
        public int NoAppThursday { get; set; }

        [Required(ErrorMessage = "Les heures de vendredi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de vendredi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Vendredi")]
        public int NoAppFriday { get; set; }

        [Required(ErrorMessage = "Les heures de samedi sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de samedi doivent etre entre {1} et {2} heures.")]
        [DisplayName("Samedi")]
        public int NoAppSaturday { get; set; }

        [Required(ErrorMessage = "Les heures de dimanche sont requises.")]
        [Range(0, 15, ErrorMessage = "Les heures de dimanche doivent etre entre {1} et {2} heures.")]
        [DisplayName("Dimanche")]
        public int NoAppSunday { get; set; }

            public string MondayStatus { get; set; }
            public string TuesdayStaus { get; set; }
            public string WednesdayStatus { get; set; }
            public string ThursdayStatus { get; set; }
            public string FridayStatus { get; set; }
            public string SaturdayStatus { get; set; }
            public string SundayStatus { get; set; }
            public int AppMonday { get; set; }
            public int AppTuesday { get; set; }
            public int AppWednesday { get; set; }
            public int AppThursday { get; set; }
            public int AppFriday { get; set; }
            public int AppSaturday { get; set; }
            public int AppSunday { get; set; }

        [DisplayName("Bonus")]
        public int Bonus { get; set; }

        [DisplayName("TH. App")]

        public int TotalApp { get; set; }
        [DisplayName("TH. Non app")]

        public int TotalNoApp { get; set; }

            public int WeekId { get; set; }
            [JsonIgnore]

            public virtual Semaine Week { get; set; }
            public int? UserId { get; set; }
            [JsonIgnore]

            public virtual Utilisateurs User { get; set; }

    }
}
