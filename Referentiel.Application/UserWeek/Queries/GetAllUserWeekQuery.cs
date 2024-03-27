using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeek.Queries
{
    public class GetAllUserWeekQuery : IRequest<List<UserWeekEntity>>
    {

        public GetAllUserWeekQuery()
        {
        }
    }
}
