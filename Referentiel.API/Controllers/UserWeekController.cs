using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Referentiel.Application.UserWeek.Commands;
using Referentiel.Application.UserWeek.Queries;
using Referentiel.Domain.Entities;
using Referentiel.Application.UserWeekProject.Commands;

namespace Referentiel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserWeekController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserWeekController> _logger;

        public UserWeekController(ILogger<UserWeekController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtient tous les UserWeeks.
        /// </summary>
        /// <response code="200">Renvoie la collection d'entités</response>
        /// <response code="404"> Données non trouvées</response>
        /// 

        [HttpGet]
        [Route("api/[controller]/[action]/{id_user}")]
        [ProducesResponseType(typeof(UserWeekEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserWeekEntity>> GetWeeksForSpecUser([FromRoute] int id_user)
        {
            try
            {
                var result = await _mediator.Send(new GetWeeksForSpecUserQuery(id_user));
                if (result == null)
                {
                    _logger.LogInformation("GET /UserWeek/{id_user} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /UserWeek/{id_user} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }



        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserWeekEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserWeekEntity>>> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAllUserWeekQuery());
                if (result == null || !result.Any())
                {
                    _logger.LogInformation("GET /UserWeek - erreur : Données introuvables.");
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

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(UserWeekEntity), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<UserWeekEntity>> GetById(int id)
        //{
        //    try
        //    {
        //        var result = await _mediator.Send(new GetOneUserWeekQuery(id));
        //        if (result == null)
        //        {
        //            _logger.LogInformation("GET /UserWeek/{id} - erreur : Données introuvables.");
        //            return NotFound();
        //        }

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "GET /UserWeek/{id} - erreur : ");
        //        return Problem("Erreur interne du serveur");
        //    }
        //}


        [HttpGet]
        [Route("api/[controller]/[action]/{id_user}/{id_week}")]
        [ProducesResponseType(typeof(UserWeekEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserWeekEntity>> GetById([FromRoute] int id_user, int id_week)
        {
            try
            {
                var result = await _mediator.Send(new GetOneUserWeekQuery(id_user,id_week));
                if (result == null)
                {
                    _logger.LogInformation("GET /UserWeek/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /UserWeek/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }


        [HttpGet]
        [Route("api/[controller]/[action]/{id_user}/{id_week}")]
        [ProducesResponseType(typeof(UserWeekEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserWeekEntity>> GetAllProjectsForThisWeek([FromRoute] int id_user, int id_week)
        {
            try
            {
                var result = await _mediator.Send(new GetAllProjectsForThisWeekQuery(id_user, id_week));
                if (result == null)
                {
                    _logger.LogInformation("GET /UserWeek/{id} - erreur : Données introuvables.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /UserWeek/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }


        //[HttpPost]
        //[ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> Create([FromBody] AddUserWeekCommand command)
        //{
        //    try
        //    {
        //        if (command == null)
        //        {
        //            _logger.LogInformation("POST /UserWeek - erreur : L'objet UserWeek est invalide.");
        //            return BadRequest();
        //        }

        //        var userweekCreated = await _mediator.Send(command);
        //        return CreatedAtAction(nameof(Get), new { userweekCreated.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "POST Create /UserWeek - erreur : ");
        //        return Problem("Erreur interne du serveur");
        //    }
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Update(int id, UpdateUserWeekCommand command)
        //{
        //    try
        //    {
        //        if (command == null)
        //        {
        //            _logger.LogInformation("PUT /UserWeek/{id} - erreur : L'objet UserWeek est invalide.");
        //            return BadRequest();
        //        }

        //        if (id < 0 || command.Id != id)
        //        {
        //            _logger.LogInformation("PUT /UserWeek/{id} - erreur : L'identifiant de l'objet est invalide.");
        //            return BadRequest();
        //        }

        //        await _mediator.Send(command);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "PUT Update /UserWeek/{id} - erreur : ");
        //        return Problem("Erreur interne du serveur");
        //    }
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (id < 0)
        //        {
        //            _logger.LogInformation("DELETE /UserWeek/{id} - erreur : L'identifiant de l'objet est invalide.");
        //            return BadRequest();
        //        }

        //        await _mediator.Send(new DeleteUserWeekCommand(id));
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "DELETE /UserWeek/{id} - erreur : ");
        //        return Problem("Erreur interne du serveur");
        //    }
        //}


        /* ----- Routes created by me ----- */
        /* ----- Routes created by me ----- */
        /* ----- Routes created by me ----- */

        ///* ----- Afficher tous les weeks d'un user spécifique ----- */
        //[HttpGet]
        //[Route("api/[controller]/[action]/{id_user}")]
        //[ProducesResponseType(typeof(IReadOnlyCollection<UserWeekEntity>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<IEnumerable<UserWeekEntity>>> GetWeeksOfUser([FromRoute]int id_user)
        //{
        //    try
        //    {
        //        var result = await _mediator.Send(new GetAllUserWeeksByUserIdQuery(id_user));
        //        if (result == null || !result.Any())
        //        {
        //            _logger.LogInformation("GET /User - erreur : Données introuvables.");
        //            return NotFound();
        //        }

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "GET /{entity.Name} - erreur : ");
        //        return Problem("Erreur interne du serveur");
        //    }
        //}


        /* ----- Ajouter une week qui n'existe pas déjà à un user spécifique avec un projet ----- */
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddUserWeekWithProjectCommand command_UW)
        {
            try
            {
                if (command_UW == null)
                {
                    _logger.LogInformation("POST /UserWeek - erreur : L'objet UserWeek est invalide.");
                    return BadRequest();
                }

                var result = await _mediator.Send(new GetWeeksForSpecUserQuery(command_UW.UserId));
                foreach (var item in result)
                {
                    if (item.WeekNumber == command_UW.WeekNumber)
                    {
                        return BadRequest("Cette semaine est déjà existant!");
                    }
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


        /* ----- Supprimer une week à un user spécifique (les projets seront supprimés automatiquement) ----- */
        [HttpDelete]
        [Route("api/[controller]/[action]/{id_user}/{id_week}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id_user, int id_week)
        {
            try
            {
                if (id_user < 0 || id_week < 0)
                {
                    _logger.LogInformation("DELETE /UserWeek/{id} - erreur : L'identifiant de l'objet est invalide.");
                    return BadRequest();
                }

                await _mediator.Send(new DeleteUserWeekCommand(id_user,id_week));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /UserWeek/{id} - erreur : ");
                return Problem("Erreur interne du serveur");
            }
        }
    }
}
