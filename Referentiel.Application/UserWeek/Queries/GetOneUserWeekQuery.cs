using MediatR;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeek.Queries
{
    public class GetOneUserWeekQuery : IRequest<UserWeekEntity>
    {
        public int IdUser { get; set; }
        public int IdWeek { get; set; }

        public GetOneUserWeekQuery(int id_user, int id_week)
        {
            this.IdUser = id_user;
            IdWeek = id_week;
        }
    }
}
