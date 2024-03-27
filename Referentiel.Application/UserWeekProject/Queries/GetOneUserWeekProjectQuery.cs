using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeekProject.Queries
{
    public class GetOneUserWeekProjectQuery : IRequest<UserWeekProjectEntity>
    {
        public int Id { get; set; }

        public GetOneUserWeekProjectQuery(int UserWeekProjectId)
        {
            this.Id = UserWeekProjectId;
        }
    }
}
