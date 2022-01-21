using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LogInApi.Controllers {
    public interface ICollaboratorController {
        Task<IActionResult> Deactivate(string cpf);
        Task<ActionResult> Delete(string cpf);
        Task<ActionResult<CollaboratorDto>> Get(string cpf);
        Task<ActionResult<CollaboratorDto>> GetByName(string fullName);
        Task<ActionResult<CollaboratorDto>> GetByNameDeactivated(string fullName);
        Task<ActionResult> GetCollaboratorsPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] OrderCollaboratorColumn orderColumn, [FromQuery] OrderType orderType = OrderType.ASC);
        Task<ActionResult<CollaboratorDto>> GetDeactivated(string cpf);
        Task<ActionResult> GetDeactivatedCollaboratorsPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] OrderCollaboratorColumn orderColumn, [FromQuery] OrderType orderType = OrderType.ASC);
        Task<ActionResult<CollaboratorDto>> Post([FromBody] CreateCollaboratorDto collaborator);
        Task<IActionResult> Put(string cpf, UpdateCollaboratorDto collaborator);
    }
}