using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeek.Commands
{
    public class  AddUserWeekWithProjectCommand : IRequest<UserWeekEntity>
    {
        public AddUserWeekWithProjectCommand(int id, string weekNumber, string statusWeek, int userId, string projectName, string quotation, int noAppMonday, int noAppTuesday, int noAppWednesday, int noAppThursday, int noAppFriday, int noAppSaturday, int noAppSunday, string mondayStatus, string tuesdayStaus, string wednesdayStatus, string thursdayStatus, string fridayStatus, string saturdayStatus, string sundayStatus, int appMonday, int appTuesday, int appWednesday, int appThursday, int appFriday, int appSaturday, int appSunday, int bonus, int totalApp, int totalNoApp, int weekId)
        {
            Id = id;
            WeekNumber = weekNumber;
            StatusWeek = statusWeek;
            ProjectName = projectName;
            Quotation = quotation;
            NoAppMonday = noAppMonday;
            NoAppTuesday = noAppTuesday;
            NoAppWednesday = noAppWednesday;
            NoAppThursday = noAppThursday;
            NoAppFriday = noAppFriday;
            NoAppSaturday = noAppSaturday;
            NoAppSunday = noAppSunday;
            MondayStatus = mondayStatus;
            TuesdayStaus = tuesdayStaus;
            WednesdayStatus = wednesdayStatus;
            ThursdayStatus = thursdayStatus;
            FridayStatus = fridayStatus;
            SaturdayStatus = saturdayStatus;
            SundayStatus = sundayStatus;
            AppMonday = appMonday;
            AppTuesday = appTuesday;
            AppWednesday = appWednesday;
            AppThursday = appThursday;
            AppFriday = appFriday;
            AppSaturday = appSaturday;
            AppSunday = appSunday;
            Bonus = bonus;
            TotalApp = totalApp;
            TotalNoApp = totalNoApp;
            UserId = userId;
            WeekId = weekId;
        }

        public int Id { get; set; }
        public string WeekNumber { get; set; }
        public string StatusWeek { get; set; }
        public int UserId { get; set; }
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
    }
}
