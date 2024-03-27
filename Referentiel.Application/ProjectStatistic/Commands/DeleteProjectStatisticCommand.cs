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
    public class DeleteProjectStatisticCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteProjectStatisticCommand(int ProjectStatisticId)
        {
            this.Id = ProjectStatisticId;
        }
    }
}
