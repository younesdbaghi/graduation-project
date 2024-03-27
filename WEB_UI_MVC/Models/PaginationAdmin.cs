using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WEB_UI_MVC.Models
{
    public class PaginationAdmin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La pagination est requise.")]
        [Range(5, 15, ErrorMessage = "La pagination doit etre entre {1} et {2} pages.")]
        [DisplayName("N° Pag. des publications")]
        public int PagAdminUsers { get; set; }

        [Required(ErrorMessage = "La pagination est requise.")]
        [Range(5, 15, ErrorMessage = "La pagination doit etre entre {1} et {2} pages.")]
        [DisplayName("N° Pag. des publications")]
        public int PagAdminPublications { get; set; }

        [Required(ErrorMessage = "La pagination est requise.")]
        [Range(5, 15, ErrorMessage = "La pagination doit etre entre {1} et {2} pages.")]
        [DisplayName("N° Pag. des projets")]
        public int PagAdminProjects { get; set; }

        [Required(ErrorMessage = "La pagination est requise.")]
        [Range(5, 15, ErrorMessage = "La pagination doit etre entre {1} et {2} pages.")]
        [DisplayName("N° Pag. des quotations")]
        public int PagAdminQuotations { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public virtual Utilisateurs User { get; set; }
    }
}
