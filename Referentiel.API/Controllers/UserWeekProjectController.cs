using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.UserWeekProject.Commands;
using Referentiel.Application.UserWeekProject.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Application.UserWeek.Commands;
using Referentiel.Application.UserWeek.Queries;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserWeekProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserWeekProjectController> _logger;

        public UserWeekProjectController(ILogger<UserWeekProjectController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les UserWeekProjects.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserWeekProjectEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserWeekProjectEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllUserWeekProjectQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /UserWeekProject - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(UserWeekProjectEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserWeekProjectEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOneUserWeekProjectQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /UserWeekProject/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /UserWeekProject/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddUserWeekProjectCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /UserWeekProject - erreur : L'objet UserWeekProject est invalide.");
                    return BadRequest();
                }

                var userweekprojectCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { userweekprojectCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /UserWeekProject - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        /* ----- Ajouter une week qui n'existe pas déjà à un user spécifique avec un projet ----- */
        [HttpPost]
        [Route("api/[controller]/[action]/{id_week}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddProjectToUserWeekCommand command_UW, [FromRoute] int id_week)
        {
            try
            {
                if (command_UW == null)
                {
                    _logger.LogInformation("POST /UserWeekProject - erreur : L'objet UserWeekProject est invalide.");
                    return BadRequest();
                }

                var result = await _mediator.Send(new GetAllProjectsForThisWeekQuery(command_UW.UserId, command_UW.WeekId));
                if (result.Count >= 3)
                {
                    return BadRequest("Vous ne pouvez pas dépassez 3 projets dans la meme semaine!");
                }

                var userweekprojectCreated = await _mediator.Send(command_UW);
                return CreatedAtAction(nameof(Get), new { userweekprojectCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /UserWeek - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }


        



        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateUserWeekProjectCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /UserWeekProject/{id} - erreur : L'objet UserWeekProject est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /UserWeekProject/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /UserWeekProject/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpDelete("{id_user}/{id_week}/{id_project}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id_user, int id_week, int id_project)
        {
            try
            {
                if (id_user < 0 || id_week < 0 || id_project < 0)
                {
                    _logger.LogInformation("DELETE /UserWeekProject/{id_user}/{id_week}/{id_project} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteUserWeekProjectCommand(id_user, id_week, id_project));
                var result = await _mediator.Send(new GetAllProjectsForThisWeekQuery(id_user, id_week));
                if(result.Count==0)
                {
                    await _mediator.Send(new DeleteUserWeekCommand(id_user, id_week));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /UserWeekProject/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
