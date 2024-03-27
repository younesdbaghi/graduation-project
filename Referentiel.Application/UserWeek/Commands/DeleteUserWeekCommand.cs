using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeek.Commands
{
    public class DeleteUserWeekCommand : IRequest<int>
    {
        public int IdUser { get; set; }
        public int IdWeek { get; set; }

        public DeleteUserWeekCommand(int id_user, int id_week)
        {
            this.IdUser = id_user;
            this.IdWeek = id_week;
        }
    }
}
