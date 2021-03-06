using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LogInApi.Controllers {
    public interface ICollaboratorController {
        Task<IActionResult> Activate(string cpf);
        Task<IActionResult> Deactivate(string cpf);
        Task<ActionResult<CollaboratorDto>> Get(string cpf);
        Task<ActionResult<CollaboratorDto>> GetByName(string fullName);
        Task<ActionResult<CollaboratorDto>> GetByNameDeactivated(string fullName);
        Task<ActionResult> GetCollaboratorsPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, [FromQuery] OrderType orderType = OrderType.ASC, [FromQuery] OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName, [FromQuery] string search = "");
        Task<ActionResult<CollaboratorDto>> GetDeactivated(string cpf);
        Task<ActionResult> GetDeactivatedCollaboratorsPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, [FromQuery] OrderType orderType = OrderType.ASC, [FromQuery] OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName, [FromQuery] string search = "");
        Task<ActionResult<CollaboratorDto>> Post([FromBody] CreateCollaboratorDto collaborator);
        Task<IActionResult> Put(string cpf, UpdateCollaboratorDto collaborator);
    }
}