using MediatR;
using Referentiel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Referentiel.Application.UserWeek.Queries
{
    public class GetAllProjectsForThisWeekQuery : IRequest<List<UserWeekProjectEntity>>
    {
        public int UserId { get; private set; }
        public int WeekId { get; private set; }

        public GetAllProjectsForThisWeekQuery(int id_user, int weekId)
        {
            this.UserId = id_user;
            this.WeekId = weekId;
        }
    }
}
