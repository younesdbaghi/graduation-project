using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Project.Commands
{
    public class DeleteProjectCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteProjectCommand(int ProjectId)
        {
            this.Id = ProjectId;
        }
    }
}
