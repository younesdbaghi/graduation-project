using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.Publication.Commands;
using Referentiel.Application.Publication.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Application.UserWeek.Queries;
using Referentiel.Application.User.Queries;
using Commons.Exceptions;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PublicationController> _logger;

        public PublicationController(ILogger<PublicationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les Publications.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<PublicationEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<PublicationEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllPublicationQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /Publication - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /{entity.Name} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicationEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublicationEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOnePublicationQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /Publication/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /Publication/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddPublicationCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /Publication - erreur : L'objet Publication est invalide.");
                    return BadRequest();
                }

                var publicationCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { publicationCreated.Id });
            }
            catch (ForbidenException ex)
            {
                _logger.LogError(ex, "POST Create /Publication - forbid : Forbidden ");
                return Forbid("Forbidden");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /Publication - erreur : ");
                return Problem("Erreur interne du serveur");
            }
            //try
            //{
            //    if (command_UW == null)
            //    {
            //        _logger.LogInformation("POST /UserWeekProject - erreur : L'objet UserWeekProject est invalide.");
            //        return BadRequest();
            //    }

            //    var result = await _mediator.Send(new GetAllProjectsForThisWeekQuery(command_UW.UserId, command_UW.WeekId));
            //    if (result.Count >= 3)
            //    {
            //        return BadRequest("Vous ne pouvez pas dépassez 3 projets dans la meme semaine!");
            //    }

            //    var userweekprojectCreated = await _mediator.Send(command_UW);
            //    return CreatedAtAction(nameof(Get), new { userweekprojectCreated.Id });
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "POST Create /UserWeek - erreur : ");
            //    return Problem("Erreur interne du serveur");
            //}
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdatePublicationCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /Publication/{id} - erreur : L'objet Publication est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /Publication/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }
                await _mediator.Send(command);
                return Ok();
            }
            catch (ForbidenException ex)
            {
                _logger.LogError(ex, "PUT Update /Publication/{id} - erreur : Forbidden ");
                return Forbid("Forbidden");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /Publication/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpDelete]
        [Route("api/[controller]/[action]/{id}/{id_user}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromRoute] int id_user)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogInformation("DELETE /Publication/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeletePublicationCommand(id, id_user));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /Publication/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }
    }
}
