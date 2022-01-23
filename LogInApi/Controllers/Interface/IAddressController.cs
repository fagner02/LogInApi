using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogInApi.Dtos;
using LogInApi.Enums;

namespace LogInApi.Controllers {
    public interface IAddressController {
        Task<ActionResult<AddressDto>> Activate(Guid id);
        Task<ActionResult<AddressDto>> Deactivate(Guid id);
        Task<ActionResult<AddressDto>> GetAddress(Guid id);
        Task<ActionResult> GetAddressesPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] OrderAddressColumn orderColumn = OrderAddressColumn.Id, [FromQuery] OrderType orderType = OrderType.ASC, [FromQuery] OrderAddressColumn searchColumn = OrderAddressColumn.Street, [FromQuery] string search = "");
        Task<ActionResult<AddressDto>> GetDeactivatedAddress(Guid id);
        Task<ActionResult> GetDeactivatedAddressesPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] OrderAddressColumn orderColumn = OrderAddressColumn.Id, [FromQuery] OrderType orderType = OrderType.ASC, [FromQuery] OrderAddressColumn searchColumn = OrderAddressColumn.Street, [FromQuery] string search = "");
        Task<ActionResult<CreateAddressDto>> Post(CreateAddressDto model);
        Task<IActionResult> Put(Guid id, UpdateAddressDto model);
    }
}
