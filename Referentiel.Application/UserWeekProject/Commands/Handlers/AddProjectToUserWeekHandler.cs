using MediatR;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeekProject.Commands.Handlers
{
    internal class AddProjectToUserWeekHandler : IRequestHandler<AddProjectToUserWeekCommand, UserWeekProjectEntity>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public AddProjectToUserWeekHandler(IUserWeekProjectRepository userweekprojectRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
        }

        public async Task<UserWeekProjectEntity> Handle(AddProjectToUserWeekCommand command, CancellationToken cancellationToken)
        {
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

            return await _userweekprojectRepository.AddAsync(userWeekProject);
        }
    }
}
