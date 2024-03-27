using MediatR;
using Referentiel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeek.Queries
{
    public class GetWeeksForSpecUserQuery : IRequest<List<UserWeekEntity>>
    {
        public int UserId { get; private set; }

        public GetWeeksForSpecUserQuery(int id_user)
        {
            this.UserId = id_user;
        }
    }
}
