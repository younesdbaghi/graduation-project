using MediatR;
using Referentiel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeek.Queries
{
    public class GetAllUserWeeksByUserIdQuery : IRequest<List<UserWeekEntity>>
    {
        public int UserId { get; private set; }

        public GetAllUserWeeksByUserIdQuery(int id_user)
        {
            this.UserId = id_user;
        }
    }
}
