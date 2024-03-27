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
    public class UpdatePublicationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Heure { get; set; }
        public int UserId { get; set; }

        public UpdatePublicationCommand(int Id, string Title, string Description, DateTime Date, string Heure, int UserId)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Date = DateTime.Now;
            this.Heure = Date.ToString("HH:mm");
            this.UserId = UserId;
        }
    }
}
