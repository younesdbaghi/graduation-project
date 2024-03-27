using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeekProject.Queries
{
    public class GetAllUserWeekProjectQuery : IRequest<List<UserWeekProjectEntity>>
    {

        public GetAllUserWeekProjectQuery()
        {
        }
    }
}
