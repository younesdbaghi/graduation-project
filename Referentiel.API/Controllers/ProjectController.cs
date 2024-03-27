using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.Project.Commands;
using Referentiel.Application.Project.Queries;
using Referentiel.Domain.Entities;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les Projects.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ProjectEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProjectQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /Project - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(ProjectEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOneProjectQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /Project/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /Project/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddProjectCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /Project - erreur : L'objet Project est invalide.");
                    return BadRequest();
                }

                var projectCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { projectCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /Project - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateProjectCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /Project/{id} - erreur : L'objet Project est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /Project/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /Project/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogInformation("DELETE /Project/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteProjectCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /Project/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
