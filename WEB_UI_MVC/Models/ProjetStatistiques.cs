using System.Text.Json.Serialization;

namespace WEB_UI_MVC.Models
{
    public class ProjetStatistiques
    {
        public int Id { get; set; }
        public float Progress { get; set; }
        public int? ProjectId { get; set; }
        [JsonIgnore]

        public virtual Projet Project { get; set; }
    }
}
