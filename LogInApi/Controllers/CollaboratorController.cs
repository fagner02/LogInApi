using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Enums;
using LogInApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogInApi.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase, ICollaboratorController {
        private readonly ICollaboratorService _collaboratorService;
        public CollaboratorController(ICollaboratorService collaboratorService) {
            _collaboratorService = collaboratorService;
        }

        /// <summary>
        /// This method returns a PagedList of active Collaborators
        /// </summary>
        /// <response code="200">Returns the PagedList with OK status</response>
        /// <response code="400">Returns an ERROR status due to invalid parameters</response>
        [HttpGet("Paged")]
        public async Task<ActionResult> GetCollaboratorsPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName,
            [FromQuery] OrderType orderType = OrderType.ASC,
            [FromQuery] OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName,
            [FromQuery] string search = ""

        ) {
            try {
                return Ok(await _collaboratorService.GetAllPaged(
                    pageNumber,
                    pageSize,
                    orderColumn,
                    orderType,
                    searchColumn,
                    search
                ));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method returns a PagedList of deactivated Collaborators
        /// </summary>
        /// <response code="200">Returns the PagedList with OK status</response>
        /// <response code="400">Returns an ERROR status due to invalid parameters</response>
        [HttpGet("Deactivated/Paged")]
        public async Task<ActionResult> GetDeactivatedCollaboratorsPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName,
            [FromQuery] OrderType orderType = OrderType.ASC,
            [FromQuery] OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName,
            [FromQuery] string search = ""
        ) {
            try {
                return Ok(await _collaboratorService.GetAllDeactivatedPaged(
                    pageNumber,
                    pageSize,
                    orderColumn,
                    orderType,
                    searchColumn,
                    search
                ));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method returns an active Collaborator by Cpf
        /// </summary>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpGet("ByCpf/{cpf}")]
        public async Task<ActionResult<CollaboratorDto>> Get([FromRoute] string cpf) {
            CollaboratorDto result = await _collaboratorService.GetByCpf(cpf);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This method returns a deactivated Collaborator by Cpf
        /// </summary>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpGet("Deactivated/ByCpf/{cpf}")]
        public async Task<ActionResult<CollaboratorDto>> GetDeactivated([FromRoute] string cpf) {
            CollaboratorDto result = await _collaboratorService.GetByCpfDeactivated(cpf);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This method returns an active Collaborator by Name
        /// </summary>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpGet("ByName/{fullName}")]
        public async Task<ActionResult<CollaboratorDto>> GetByName([FromRoute] string fullName) {
            CollaboratorDto result = await _collaboratorService.GetByName(fullName);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This method returns a deactivated Collaborator by Name
        /// </summary>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpGet("Deactivated/ByName/{fullName}")]
        public async Task<ActionResult<CollaboratorDto>> GetByNameDeactivated([FromRoute] string fullName) {
            CollaboratorDto result = await _collaboratorService.GetByNameDeactivated(fullName);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This method creates a new Collaborator and returns the created Collaborator
        /// </summary>
        /// <remarks>
        /// All fields can be edited.
        /// Cpf is required, must be valid and follow the format 000.000.000-00.
        /// Sex field is a single uppercase letter.
        /// </remarks>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="400">Returns an ERROR status due to validation error</response>
        [HttpPost]
        public async Task<ActionResult<CollaboratorDto>> Post([FromBody] CreateCollaboratorDto collaborator) {
            try {
                return Ok(await _collaboratorService.Create(collaborator));
            } catch (Exception e) {
                string message = e.Message;
                if (e.InnerException != null) {
                    message = $"{e.Message} {e.InnerException.Message}";
                }
                return BadRequest(message);
            }
        }

        /// <summary>
        /// This method updates a Collaborator
        /// </summary>
        /// <remarks>
        /// This method uses Cpf to search for the Collaborator.
        /// This method only searchs for active Collaborators.
        /// Only FullName, Phone, Sex and AddressId can be updated.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="400">Returns an ERROR status due to validation error</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put([FromRoute] string cpf, [FromBody] UpdateCollaboratorDto collaborator) {
            try {
                if (!await _collaboratorService.Update(cpf, collaborator)) {
                    return NotFound();
                }
            } catch (Exception e) {
                string message = e.Message;
                if (e.InnerException != null) {
                    message = $"{e.Message} {e.InnerException.Message}";
                }
                return BadRequest(message);
            }
            return NoContent();
        }

        /// <summary>
        /// This method deactivates a Collaborator
        /// </summary>
        /// <remarks>
        /// This method uses Cpf to search for the Collaborator.
        /// This method only searchs for active Collaborators.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Deactivate([FromRoute] string cpf) {
            if (!await _collaboratorService.Deactivate(cpf)) {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// This method activates a Collaborator
        /// </summary>
        /// <remarks>
        /// This method uses Cpf to search for the Collaborator.
        /// This method only searchs for deactivated Collaborators.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="404">Returns an ERROR status due to collaborator not found</response>
        [HttpPut("Deactivated/{cpf}")]
        public async Task<IActionResult> Activate([FromRoute] string cpf) {
            if (!await _collaboratorService.Activate(cpf)) {
                return NotFound();
            }
            return NoContent();
        }
    }
}