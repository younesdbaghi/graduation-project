using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.User.Commands;
using Referentiel.Application.User.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Application.UserWeekProject.Queries;
using Referentiel.Application.UserWeek.Queries;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les Users.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllUserQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /User - erreur : Données introuvables.");
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



        [HttpGet]
        [ProducesResponseType(typeof(UserEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [Route("api/[controller]/[action]/{id}")]
        public async Task<ActionResult<UserEntity>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetOneUserQuery(id));
                if (result == null)
                {
                    _logger.LogInformation("GET /User/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /User/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(UserEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [Route("api/[controller]/[action]/{username}")]
        public async Task<ActionResult<UserEntity>> GetByUsername([FromRoute]string username)
        {
            try
            {
                var result = await _mediator.Send(new GetUsersByUsernameQuery(username));
                if (result == null)
                {
                    _logger.LogInformation("GET /User/{username} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /User/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddUserCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("POST /User - erreur : L'objet User est invalide.");
                    return BadRequest();
                }

                var userCreated = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { userCreated.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Create /User - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateUserCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogInformation("PUT /User/{id} - erreur : L'objet User est invalide.");
                    return BadRequest();
                }

                if (id < 0 || command.Id != id)
                {
                    _logger.LogInformation("PUT /User/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Update /User/{id} - erreur : ");
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
                    _logger.LogInformation("DELETE /User/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteUserCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /User/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }
    }
}
