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

        /// <summary>
        /// This method returns a PagedList of active Addresses
        /// </summary>
        /// <response code="200">Returns the PagedList with OK status</response>
        /// <response code="400">Returns an ERROR status due to invalid parameters</response>
        [HttpGet("Paged")]
        public async Task<ActionResult> GetAddressesPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] OrderAddressColumn orderColumn = OrderAddressColumn.Id,
            [FromQuery] OrderType orderType = OrderType.ASC,
            [FromQuery] OrderAddressColumn searchColumn = OrderAddressColumn.Street,
            [FromQuery] string search = ""
        ) {
            return Ok(await _addressService.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            ));
        }

        /// <summary>
        /// This method returns a PagedList of deactivated Addresses
        /// </summary>
        /// <response code="200">Returns the PagedList with OK status</response>
        /// <response code="400">Returns an ERROR status due to invalid parameters</response>
        [HttpGet("Deactivated/Paged")]
        public async Task<ActionResult> GetDeactivatedAddressesPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] OrderAddressColumn orderColumn = OrderAddressColumn.Id,
            [FromQuery] OrderType orderType = OrderType.ASC,
            [FromQuery] OrderAddressColumn searchColumn = OrderAddressColumn.Street,
            [FromQuery] string search = ""
        ) {
            return Ok(await _addressService.GetAllDeactivatedPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            ));
        }

        /// <summary>
        /// This method returns an active Address by Id
        /// </summary>
        /// <response code="200">Returns the Address with OK status</response>
        /// <response code="404">Returns an ERROR status due to Address not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress([FromRoute] Guid id) {
            AddressDto address = await _addressService.Get(id);
            if (address == null) {
                return NotFound();
            }
            return Ok(address);
        }

        /// <summary>
        /// This method returns a deactivated Address by Id
        /// </summary>
        /// <response code="200">Returns the Collaborator with OK status</response>
        /// <response code="404">Returns an ERROR status due to Address not found</response>
        [HttpGet("Deactivated/{id}")]
        public async Task<ActionResult<AddressDto>> GetDeactivatedAddress([FromRoute] Guid id) {
            AddressDto address = await _addressService.GetDeativated(id);
            if (address == null) {
                return NotFound();
            }
            return Ok(address);
        }

        /// <summary>
        /// This method creates a new Address
        /// </summary>
        /// <response code="200">Returns the Address with OK status</response>
        /// <response code="400">Returns an ERROR status due to validation error</response>
        [HttpPost]
        public async Task<ActionResult<CreateAddressDto>> Post([FromBody] CreateAddressDto model) {
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

        /// <summary>
        /// This method updates an Address
        /// </summary>
        /// <remarks>
        /// This method only searchs for active Addresses.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="400">Returns an ERROR status due to validation error</response>
        /// <response code="404">Returns an ERROR status due to Address not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateAddressDto model) {
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

        /// <summary>
        /// This method deactivates a Collaborator
        /// </summary>
        /// <remarks>
        /// This method only searchs for active Addresses.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="404">Returns an ERROR status due to Address not found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressDto>> Deactivate([FromRoute] Guid id) {
            if (!await _addressService.Deactivate(id)) {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// This method activates a Collaborator
        /// </summary>
        /// <remarks>
        /// This method only searchs for deactivated Addresses.
        /// </remarks>
        /// <response code="204">Returns an OK status with no content</response>
        /// <response code="404">Returns an ERROR status due to Address not found</response>
        [HttpPut("Deactivated/{id}")]
        public async Task<ActionResult<AddressDto>> Activate([FromRoute] Guid id) {
            if (!await _addressService.Activate(id)) {
                return NotFound();
            }
            return NoContent();
        }
    }
}
