using System;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Enums;

namespace LogInApi.Services {
    public interface IAddressService {
        Task<bool> Activate(Guid id);
        Task Create(CreateAddressDto address);
        Task<bool> Deactivate(Guid id);
        Task<AddressDto> Get(Guid id);
        Task<Response<AddressDto>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType);
        Task<Response<AddressDto>> GetAllPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType);
        Task<AddressDto> GetDeativated(Guid id);
        Task<bool> Update(Guid id, UpdateAddressDto address);
    }
}