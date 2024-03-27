using MediatR;
using Referentiel.Application.UserWeekProject.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeekProject.Commands.Handlers
{
    public class UpdateUserWeekProjectHandler : IRequestHandler<UpdateUserWeekProjectCommand, int>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public UpdateUserWeekProjectHandler(IUserWeekProjectRepository userweekprojectRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
        }

        public async Task<int> Handle(UpdateUserWeekProjectCommand command, CancellationToken cancellationToken)
        {
            command.TotalNoApp = command.NoAppMonday + command.NoAppTuesday + command.NoAppWednesday +
                                  command.NoAppThursday + command.NoAppFriday + command.NoAppSaturday +
                                  command.NoAppSunday;

            command.TotalApp = command.AppMonday + command.AppTuesday + command.AppWednesday +
                                  command.AppThursday + command.AppFriday + command.AppSaturday +
                                  command.AppSunday;
            var userweekproject = new UserWeekProjectEntity()
            {
                Id = command.Id,
                ProjectName = command.ProjectName,
                Quotation = command.Quotation,
                NoAppMonday = command.NoAppMonday,
                NoAppTuesday = command.NoAppTuesday,
                NoAppWednesday = command.NoAppWednesday,
                NoAppThursday = command.NoAppThursday,
                NoAppFriday = command.NoAppFriday,
                NoAppSaturday = command.NoAppSaturday,
                NoAppSunday = command.NoAppSunday,
                MondayStatus = command.MondayStatus,
                TuesdayStaus = command.TuesdayStaus,
                WednesdayStatus = command.WednesdayStatus,
                ThursdayStatus = command.ThursdayStatus,
                FridayStatus = command.FridayStatus,
                SaturdayStatus = command.SaturdayStatus,
                SundayStatus = command.SundayStatus,
                AppMonday = command.AppMonday,
                AppTuesday = command.AppTuesday,
                AppWednesday = command.AppWednesday,
                AppThursday = command.AppThursday,
                AppFriday = command.AppFriday,
                AppSaturday = command.AppSaturday,
                AppSunday = command.AppSunday,
                Bonus = command.Bonus,
                TotalApp = command.TotalApp,
                TotalNoApp = command.TotalNoApp,
                WeekId = command.WeekId,
                UserId = command.UserId,
            };

            return await _userweekprojectRepository.UpdateAsync(userweekproject);
        }
    }
}
