using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Referentiel.Domain.Entities;

namespace Referentiel.Application.Publication.Commands
{
    public class DeletePublicationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DeletePublicationCommand(int PublicationId, int idUser)
        {
            this.Id = PublicationId;
            this.IdUser = idUser;
        }
    }
}
