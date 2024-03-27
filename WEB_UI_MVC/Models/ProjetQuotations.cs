using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WEB_UI_MVC.Models
{
    public class ProjetQuotations
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La quotation est requise.")]
        [MaxLength(100, ErrorMessage = "La quotation ne doit avoir que {1} caractères au maximum.")]
        [DisplayName("Quotation")]
        public string Quotation { get; set; }
        public int? ProjectId { get; set; }

        [JsonIgnore]
        public virtual Projet? Project { get; set; }
    }
}
