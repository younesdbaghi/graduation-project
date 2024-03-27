using MediatR;
using Referentiel.Application.UserWeekProject.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeekProject.Commands.Handlers
{
    public class AddUserWeekProjectHandler : IRequestHandler<AddUserWeekProjectCommand, UserWeekProjectEntity>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;

        public AddUserWeekProjectHandler(IUserWeekProjectRepository userweekprojectRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
        }

        public async Task<UserWeekProjectEntity> Handle(AddUserWeekProjectCommand command, CancellationToken cancellationToken)
        {
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

            return await _userweekprojectRepository.AddAsync(userweekproject);
        }
    }
}
