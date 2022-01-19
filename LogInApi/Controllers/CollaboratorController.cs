using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Enums;
using LogInApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogInApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorController : ControllerBase {
        private readonly ICollaboratorService _collaboratorService;
        public CollaboratorController(ICollaboratorService collaboratorService) {
            _collaboratorService = collaboratorService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CollaboratorDto>>> GetAll() {
            return Ok(await _collaboratorService.GetAll());
        }

        [HttpGet("Paged")]
        public async Task<ActionResult> GetAllPaged(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] OrderCollaboratorColumn orderColumn,
            [FromQuery] OrderType orderType = OrderType.ASC
        ) {
            return Ok(await _collaboratorService.GetAllPaged(pageNumber, pageSize, orderColumn, orderType));
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<CollaboratorDto>> Get(string cpf) {
            return Ok(await _collaboratorService.GetByCpf(cpf));
        }

        [HttpGet("{fullName}")]
        public async Task<ActionResult<CollaboratorDto>> GetByName(string fullName) {
            return Ok(await _collaboratorService.GetByName(fullName));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CollaboratorDto collaborator) {
            await _collaboratorService.Create(collaborator);
            return Ok();
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put(string cpf, UpdateCollaboratorDto collaborator) {
            if (!await _collaboratorService.Update(cpf, collaborator)) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> Delete(string cpf) {
            if (!await _collaboratorService.Delete(cpf)) {
                return NotFound();
            }
            return NoContent();
        }
    }
}