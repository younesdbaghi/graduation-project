using MediatR;
using Referentiel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeekProject.Commands
{
    public class AddProjectToUserWeekCommand : IRequest<UserWeekProjectEntity>
    {
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
        public int UserId { get; set; }

        public AddProjectToUserWeekCommand(int Id, string ProjectName, string Quotation, int NoAppMonday, int NoAppTuesday, int NoAppWednesday, int NoAppThursday, int NoAppFriday, int NoAppSaturday, int NoAppSunday, string MondayStatus, string TuesdayStaus, string WednesdayStatus, string ThursdayStatus, string FridayStatus, string SaturdayStatus, string SundayStatus, int AppMonday, int AppTuesday, int AppWednesday, int AppThursday, int AppFriday, int AppSaturday, int AppSunday, int Bonus, int TotalApp, int TotalNoApp, int WeekId, int UserId)
        {
            this.Id = Id;
            this.ProjectName = ProjectName;
            this.Quotation = Quotation;
            this.NoAppMonday = NoAppMonday;
            this.NoAppTuesday = NoAppTuesday;
            this.NoAppWednesday = NoAppWednesday;
            this.NoAppThursday = NoAppThursday;
            this.NoAppFriday = NoAppFriday;
            this.NoAppSaturday = NoAppSaturday;
            this.NoAppSunday = NoAppSunday;
            this.MondayStatus = MondayStatus;
            this.TuesdayStaus = TuesdayStaus;
            this.WednesdayStatus = WednesdayStatus;
            this.ThursdayStatus = ThursdayStatus;
            this.FridayStatus = FridayStatus;
            this.SaturdayStatus = SaturdayStatus;
            this.SundayStatus = SundayStatus;
            this.AppMonday = AppMonday;
            this.AppTuesday = AppTuesday;
            this.AppWednesday = AppWednesday;
            this.AppThursday = AppThursday;
            this.AppFriday = AppFriday;
            this.AppSaturday = AppSaturday;
            this.AppSunday = AppSunday;
            this.Bonus = Bonus;
            this.TotalApp = TotalApp;
            this.TotalNoApp = TotalNoApp;
            this.WeekId = WeekId;
            this.UserId = UserId;
        }
    }
}
