using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.ProjectStatistic.Commands;
using Referentiel.Application.ProjectStatistic.Queries;
using Referentiel.Domain.Entities;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectStatisticController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProjectStatisticController> _logger;

        public ProjectStatisticController(ILogger<ProjectStatisticController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les ProjectStatistics.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ProjectStatisticEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectStatisticEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProjectStatisticQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /ProjectStatistic - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(ProjectStatisticEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectStatisticEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOneProjectStatisticQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /ProjectStatistic/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /ProjectStatistic/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddProjectStatisticCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /ProjectStatistic - erreur : L'objet ProjectStatistic est invalide.");
                    return BadRequest();
                }

                var projectstatisticCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { projectstatisticCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /ProjectStatistic - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateProjectStatisticCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /ProjectStatistic/{id} - erreur : L'objet ProjectStatistic est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /ProjectStatistic/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /ProjectStatistic/{id} - erreur : ");
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
                    _logger.LogInformation("DELETE /ProjectStatistic/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteProjectStatisticCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /ProjectStatistic/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
