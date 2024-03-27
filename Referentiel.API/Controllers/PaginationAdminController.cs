using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.PaginationAdmin.Commands;
using Referentiel.Application.PaginationAdmin.Queries;
using Referentiel.Domain.Entities;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaginationAdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaginationAdminController> _logger;

        public PaginationAdminController(ILogger<PaginationAdminController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les PaginationAdmins.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<PaginationAdminEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<PaginationAdminEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllPaginationAdminQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /PaginationAdmin - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(PaginationAdminEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PaginationAdminEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOnePaginationAdminQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /PaginationAdmin/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /PaginationAdmin/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddPaginationAdminCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /PaginationAdmin - erreur : L'objet PaginationAdmin est invalide.");
                    return BadRequest();
                }

                var paginationadminCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { paginationadminCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /PaginationAdmin - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdatePaginationAdminCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /PaginationAdmin/{id} - erreur : L'objet PaginationAdmin est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /PaginationAdmin/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /PaginationAdmin/{id} - erreur : ");
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
                    _logger.LogInformation("DELETE /PaginationAdmin/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeletePaginationAdminCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /PaginationAdmin/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
