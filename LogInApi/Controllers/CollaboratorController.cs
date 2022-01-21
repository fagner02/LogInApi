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
    public class CollaboratorController : ControllerBase {
        private readonly ICollaboratorService _collaboratorService;
        public CollaboratorController(ICollaboratorService collaboratorService) {
            _collaboratorService = collaboratorService;
        }

        [HttpGet("Paged")]
        public async Task<ActionResult> GetCollaboratorsPaged(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] OrderCollaboratorColumn orderColumn,
            [FromQuery] OrderType orderType = OrderType.ASC
        ) {
            return Ok(await _collaboratorService.GetAllPaged(pageNumber, pageSize, orderColumn, orderType));
        }

        [HttpGet("Deactivated/Paged")]
        public async Task<ActionResult> GetDeactivatedCollaboratorsPaged(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] OrderCollaboratorColumn orderColumn,
            [FromQuery] OrderType orderType = OrderType.ASC
        ) {
            return Ok(await _collaboratorService.GetAllDeactivatedPaged(pageNumber, pageSize, orderColumn, orderType));
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<CollaboratorDto>> Get(string cpf) {
            return Ok(await _collaboratorService.GetByCpf(cpf));
        }

        [HttpGet("Deactivated/{cpf}")]
        public async Task<ActionResult<CollaboratorDto>> GetDeactivated(string cpf) {
            return Ok(await _collaboratorService.GetByCpfDeactivated(cpf));
        }

        [HttpGet("{fullName}")]
        public async Task<ActionResult<CollaboratorDto>> GetByName(string fullName) {
            return Ok(await _collaboratorService.GetByName(fullName));
        }

        [HttpGet("Deactivated/{fullName}")]
        public async Task<ActionResult<CollaboratorDto>> GetByNameDeactivated(string fullName) {
            return Ok(await _collaboratorService.GetByNameDeactivated(fullName));
        }

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

        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put(string cpf, UpdateCollaboratorDto collaborator) {
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

        [HttpDelete("Deactivated/{cpf}")]
        public async Task<ActionResult> Delete(string cpf) {
            if (!await _collaboratorService.Delete(cpf)) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Deactivate(string cpf) {
            if (!await _collaboratorService.Deactivate(cpf)) {
                return NotFound();
            }
            return NoContent();
        }
    }
}