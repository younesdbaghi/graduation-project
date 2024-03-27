using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectStatistic.Queries
{
    public class GetOneProjectStatisticQuery : IRequest<ProjectStatisticEntity>
    {
        public int Id { get; set; }

        public GetOneProjectStatisticQuery(int ProjectStatisticId)
        {
            this.Id = ProjectStatisticId;
        }
    }
}
