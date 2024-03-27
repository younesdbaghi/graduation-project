using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.UserWeekProject.Commands
{
    public class DeleteUserWeekProjectCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int WeekId { get; set; }
        public int ProjectId { get; set; }


        public DeleteUserWeekProjectCommand(int id_user, int id_week, int id_project)
        {
            this.UserId = id_user;
            this.WeekId = id_week;
            this.ProjectId = id_project;
        }
    }
}
