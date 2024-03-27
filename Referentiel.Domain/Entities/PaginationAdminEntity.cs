using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Referentiel.Domain.Entities
{
    public class PaginationAdminEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int PagAdminUsers { get; set; }
        public int PagAdminPublications { get; set; }
        public int PagAdminProjects { get; set; }
        public int PagAdminQuotations { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
