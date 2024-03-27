using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Referentiel.Domain.Entities
{
    public class UserWeekEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string WeekNumber { get; set; }
        public string StatusWeek { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }
        public virtual List<UserWeekProjectEntity> UserWeekProjects { get; set; }
    }
}
