using MediatR;
using Referentiel.Application.UserWeekProject.Commands;
using Referentiel.Domain.Entities;
using Referentiel.Infrastructure.Repositories.Interfaces;

namespace Referentiel.Application.UserWeekProject.Commands.Handlers
{
    public class DeleteUserWeekProjectHandler : IRequestHandler<DeleteUserWeekProjectCommand, int>
    {
        private readonly IUserWeekProjectRepository _userweekprojectRepository;
        private readonly IUserWeekRepository _userweekRepository;


        public DeleteUserWeekProjectHandler(IUserWeekProjectRepository userweekprojectRepository, IUserWeekRepository userWeekRepository)
        {
            _userweekprojectRepository = userweekprojectRepository;
            _userweekRepository = userWeekRepository;
        }

        public async Task<int> Handle(DeleteUserWeekProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = await _userweekRepository.GetAllProjectsForThisWeek(command.UserId,command.WeekId);
            var objectForDelete = entity.Where(uwp => uwp.Id == command.ProjectId).FirstOrDefault();
            return await _userweekprojectRepository.DeleteAsync(objectForDelete);
        }
    }
}
