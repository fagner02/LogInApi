using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogInApi.Services;
using LogInApi.Dtos;
using LogInApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogInApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase, IAddressController {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService) {
            _addressService = addressService;
        }

        [HttpGet("Paged")]
        public async Task<ActionResult> GetAdressesPaged(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] OrderAddressColumn orderColumn,
            [FromQuery] OrderType orderType = OrderType.ASC
        ) {
            return Ok(await _addressService.GetAllPaged(pageNumber, pageSize, orderColumn, orderType));
        }

        [HttpGet("Deactivated/Paged")]
        public async Task<ActionResult> GetDeactivatedAdressesPaged(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] OrderAddressColumn orderColumn,
            [FromQuery] OrderType orderType = OrderType.ASC
        ) {
            return Ok(await _addressService.GetAllDeactivatedPaged(pageNumber, pageSize, orderColumn, orderType));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress(Guid id) {
            AddressDto address = await _addressService.Get(id);
            if (address == null) {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpGet("Deactivated/{id}")]
        public async Task<ActionResult<AddressDto>> GetDeactivatedAddress(Guid id) {
            AddressDto address = await _addressService.GetDeativated(id);
            if (address == null) {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<CreateAddressDto>> Post(CreateAddressDto model) {
            try {
                await _addressService.Create(model);
            } catch (Exception e) {
                string message = e.Message;
                if (e.InnerException != null) {
                    message = $"{e.Message} {e.InnerException.Message}";
                }
                return BadRequest(message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateAddressDto model) {
            try {
                if (!await _addressService.Update(id, model)) {
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressDto>> Deactivate(Guid id) {
            if (!await _addressService.Deactivate(id)) {
                return NotFound();
            }
            return NoContent();
        }
    }
}
