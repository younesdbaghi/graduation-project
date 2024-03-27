using MediatR;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories;
using Referentiel.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Referentiel.Application.UserWeek.Commands.Handlers
{
    internal class AddUserWeekWithProjectHandler : IRequestHandler<AddUserWeekWithProjectCommand, UserWeekEntity>
    {
        private readonly IUserWeekRepository _userweekRepository;
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public AddUserWeekWithProjectHandler(IUserWeekProjectRepository userweekprojectRepository, IUserWeekRepository userweekRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
            _userweekRepository = userweekRepository;
        }

        public async Task<UserWeekEntity> Handle(AddUserWeekWithProjectCommand command, CancellationToken cancellationToken)
        {
            command.StatusWeek = "OUVERT";
            var userweek = new UserWeekEntity()
            {
                WeekNumber = command.WeekNumber,
                StatusWeek = command.StatusWeek,
                UserId = command.UserId
            };

            var traitement_userweek = await _userweekRepository.AddAsync(userweek);


            /* Valeurs par défaut */
            command.MondayStatus = "Non approuvé";
            command.TuesdayStaus = "Non approuvé";
            command.WednesdayStatus = "Non approuvé";
            command.ThursdayStatus = "Non approuvé";
            command.FridayStatus = "Non approuvé";
            command.SaturdayStatus = "Non approuvé";
            command.SundayStatus = "Non approuvé";
            command.AppMonday = 0;
            command.AppTuesday = 0;
            command.AppWednesday = 0;
            command.AppThursday = 0;
            command.AppFriday = 0;
            command.AppSaturday = 0;
            command.AppSunday = 0;
            command.TotalNoApp = command.NoAppMonday + command.NoAppTuesday + command.NoAppWednesday +
                                  command.NoAppThursday + command.NoAppFriday + command.NoAppSaturday +
                                  command.NoAppSunday;
            command.TotalApp = 0;
            command.Bonus = (command.TotalNoApp + command.TotalApp) - 40;
            if (!(command.Bonus > 0))
            {
                command.Bonus = 0;
            }


            int lastWeekId = 0;
            var weeks = await _userweekRepository.GetWeeksForSpecUserAsync(command.UserId);
            foreach ( var week in weeks )
            {
                lastWeekId = week.Id;
                break;
            }

            command.WeekId = lastWeekId;




            var userWeekProject = new UserWeekProjectEntity();
            userWeekProject.ProjectName = command.ProjectName;
            userWeekProject.Quotation = command.Quotation;
            userWeekProject.NoAppMonday = command.NoAppMonday;
            userWeekProject.NoAppTuesday = command.NoAppTuesday;
            userWeekProject.NoAppWednesday = command.NoAppWednesday;
            userWeekProject.NoAppThursday = command.NoAppThursday;
            userWeekProject.NoAppFriday = command.NoAppFriday;
            userWeekProject.NoAppSaturday = command.NoAppSaturday;
            userWeekProject.NoAppSunday = command.NoAppSunday;
            userWeekProject.MondayStatus = command.MondayStatus;
            userWeekProject.TuesdayStaus = command.TuesdayStaus; // Correction du nom de propriété
            userWeekProject.WednesdayStatus = command.WednesdayStatus;
            userWeekProject.ThursdayStatus = command.ThursdayStatus;
            userWeekProject.FridayStatus = command.FridayStatus;
            userWeekProject.SaturdayStatus = command.SaturdayStatus;
            userWeekProject.SundayStatus = command.SundayStatus;
            userWeekProject.AppMonday = command.AppMonday;
            userWeekProject.AppTuesday = command.AppTuesday;
            userWeekProject.AppWednesday = command.AppWednesday;
            userWeekProject.AppThursday = command.AppThursday;
            userWeekProject.AppFriday = command.AppFriday;
            userWeekProject.AppSaturday = command.AppSaturday;
            userWeekProject.AppSunday = command.AppSunday;
            userWeekProject.Bonus = command.Bonus;
            userWeekProject.TotalApp = command.TotalApp;
            userWeekProject.TotalNoApp = command.TotalNoApp;
            userWeekProject.UserId = command.UserId;
            userWeekProject.WeekId = command.WeekId;


            var traitement_userweekproject = await _userweekprojectRepository.AddAsync(userWeekProject);

            return traitement_userweek;
        }
    }
}
