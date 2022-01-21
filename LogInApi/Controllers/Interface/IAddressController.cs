using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogInApi.Dtos;
using LogInApi.Enums;

namespace LogInApi.Controllers {
    public interface IAddressController {
        Task<ActionResult<AddressDto>> Deactivate(Guid id);
        Task<ActionResult<AddressDto>> Delete(Guid id);
        Task<ActionResult<AddressDto>> GetAddress(Guid id);
        Task<ActionResult> GetAdressesPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] OrderAddressColumn orderColumn, [FromQuery] OrderType orderType = OrderType.ASC);
        Task<ActionResult<AddressDto>> GetDeactivatedAddress(Guid id);
        Task<ActionResult> GetDeactivatedAdressesPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] OrderAddressColumn orderColumn, [FromQuery] OrderType orderType = OrderType.ASC);
        Task<ActionResult<CreateAddressDto>> Post(CreateAddressDto model);
        Task<IActionResult> Put(Guid id, CreateAddressDto model);
    }
}
