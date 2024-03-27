using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Referentiel.Domain.Entities
{
    public class ProjectEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int HeuresTotal { get; set; }
        public virtual List<ProjectQuotationEntity>? ProjectQuotations { get; set; }
        public virtual List<ProjectStatisticEntity> ProjectStatistics { get; set; }
    }
}
