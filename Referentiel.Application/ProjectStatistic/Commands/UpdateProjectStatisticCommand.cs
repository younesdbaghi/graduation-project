using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectStatistic.Commands
{
    public class UpdateProjectStatisticCommand : IRequest<int>
    {
        public int Id { get; set; }
        public float Progress { get; set; }
        public int ProjectId { get; set; }

        public UpdateProjectStatisticCommand(int Id, float Progress, int ProjectId)
        {
            this.Id = Id;
            this.Progress = Progress;
            this.ProjectId = ProjectId;
        }
    }
}
