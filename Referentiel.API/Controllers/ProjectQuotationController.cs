using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.ProjectQuotation.Commands;
using Referentiel.Application.ProjectQuotation.Queries;
using Referentiel.Domain.Entities;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectQuotationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProjectQuotationController> _logger;

        public ProjectQuotationController(ILogger<ProjectQuotationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les ProjectQuotations.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ProjectQuotationEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectQuotationEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProjectQuotationQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /ProjectQuotation - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(ProjectQuotationEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectQuotationEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOneProjectQuotationQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /ProjectQuotation/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /ProjectQuotation/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddProjectQuotationCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /ProjectQuotation - erreur : L'objet ProjectQuotation est invalide.");
                    return BadRequest();
                }

                var projectquotationCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { projectquotationCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /ProjectQuotation - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateProjectQuotationCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /ProjectQuotation/{id} - erreur : L'objet ProjectQuotation est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /ProjectQuotation/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /ProjectQuotation/{id} - erreur : ");
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
                    _logger.LogInformation("DELETE /ProjectQuotation/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteProjectQuotationCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /ProjectQuotation/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
