using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.PaginationUser.Commands;
using Referentiel.Application.PaginationUser.Queries;
using Referentiel.Domain.Entities;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaginationUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaginationUserController> _logger;

        public PaginationUserController(ILogger<PaginationUserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les PaginationUsers.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<PaginationUserEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<PaginationUserEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllPaginationUserQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /PaginationUser - erreur : Données introuvables.");
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
        [ProducesResponseType(typeof(PaginationUserEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PaginationUserEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOnePaginationUserQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /PaginationUser/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /PaginationUser/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddPaginationUserCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /PaginationUser - erreur : L'objet PaginationUser est invalide.");
                    return BadRequest();
                }

                var paginationuserCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { paginationuserCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /PaginationUser - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdatePaginationUserCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /PaginationUser/{id} - erreur : L'objet PaginationUser est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /PaginationUser/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /PaginationUser/{id} - erreur : ");
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
                    _logger.LogInformation("DELETE /PaginationUser/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeletePaginationUserCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /PaginationUser/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

    }
}
