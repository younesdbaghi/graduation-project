using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Referentiel.Domain.Entities
{
    public class ProjectStatisticEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public float Progress { get; set; }
        public int? ProjectId { get; set; }
        [JsonIgnore]

        public virtual ProjectEntity Project { get; set; }
    }
}
