using MediatR;
using Referentiel.Application.UserWeek.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeek.Commands.Handlers
{
    public class DeleteUserWeekHandler : IRequestHandler<DeleteUserWeekCommand, int>
    {
        private readonly IUserWeekRepository _userweekRepository;
        private readonly IUserWeekProjectRepository _userWeekProjectRepository;

        public DeleteUserWeekHandler(IUserWeekRepository userweekRepository, IUserWeekProjectRepository userWeekProjectRepository)
        {
            _userweekRepository = userweekRepository;
            _userWeekProjectRepository = userWeekProjectRepository;
        }

        public async Task<int> Handle(DeleteUserWeekCommand command, CancellationToken cancellationToken)
        {
            var UserWeekProjects = await _userweekRepository.GetAllProjectsForThisWeek(command.IdUser, command.IdWeek);
            foreach (var Project in UserWeekProjects)
            {
                await _userWeekProjectRepository.DeleteAsync(Project);
            }
            var UserWeekentity= await _userweekRepository.GetSpecWeekForSpecUser(command.IdUser,command.IdWeek);
            var DeleteUserWeek = await _userweekRepository.DeleteAsync(UserWeekentity);
            
            return DeleteUserWeek;
        }
    }
}
