using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Referentiel.Domain.Entities
{
    public class UserWeekProjectEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Quotation { get; set; }
        public int NoAppMonday { get; set; }
        public int NoAppTuesday { get; set; }
        public int NoAppWednesday { get; set; }
        public int NoAppThursday { get; set; }
        public int NoAppFriday { get; set; }
        public int NoAppSaturday { get; set; }
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
        public int Bonus { get; set; }
        public int TotalApp { get; set; }
        public int TotalNoApp { get; set; }
        public int WeekId { get; set; }
        [JsonIgnore]

        public virtual UserWeekEntity Week { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]

        public virtual UserEntity User { get; set; }

    }
}
