using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectStatistic.Queries
{
    public class GetAllProjectStatisticQuery : IRequest<List<ProjectStatisticEntity>>
    {

        public GetAllProjectStatisticQuery()
        {
        }
    }
}
