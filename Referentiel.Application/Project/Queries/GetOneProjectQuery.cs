using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Project.Queries
{
    public class GetOneProjectQuery : IRequest<ProjectEntity>
    {
        public int Id { get; set; }

        public GetOneProjectQuery(int ProjectId)
        {
            this.Id = ProjectId;
        }
    }
}
