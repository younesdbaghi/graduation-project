using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.ProjectQuotation.Commands
{
    public class DeleteProjectQuotationCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteProjectQuotationCommand(int ProjectQuotationId)
        {
            this.Id = ProjectQuotationId;
        }
    }
}
