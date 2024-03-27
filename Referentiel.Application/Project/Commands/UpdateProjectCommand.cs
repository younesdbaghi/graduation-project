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
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int HeuresTotal { get; set; }

        public UpdateProjectCommand(int Id, string ProjectName, int HeuresTotal)
        {
            this.Id = Id;
            this.ProjectName = ProjectName;
            this.HeuresTotal = HeuresTotal;
        }
    }
}
