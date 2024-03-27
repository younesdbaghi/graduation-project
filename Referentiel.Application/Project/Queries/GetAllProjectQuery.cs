using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Project.Queries
{
    public class GetAllProjectQuery : IRequest<List<ProjectEntity>>
    {

        public GetAllProjectQuery()
        {
        }
    }
}
