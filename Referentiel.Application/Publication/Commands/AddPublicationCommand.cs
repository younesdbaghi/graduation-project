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
    public class AddPublicationCommand : IRequest<PublicationEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Heure { get; set; }
        public int UserId { get; set; }

        public AddPublicationCommand(int Id, string Title, DateTime Date, string Heure, string Description, int UserId)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Date = Date;
            this.Heure = Heure;
            this.UserId = UserId;
        }
    }
}
