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
    public class UpdateProjectQuotationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Quotation { get; set; }
        public int ProjectId { get; set; }

        public UpdateProjectQuotationCommand(int Id, string Quotation, int ProjectId)
        {
            this.Id = Id;
            this.Quotation = Quotation;
            this.ProjectId = ProjectId;
        }
    }
}
