using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Enums;

namespace LogInApi.Services {
    public interface IAddressService {
        Task Create(CreateAddressDto address);
        Task<bool> Deactivate(Guid id);
        Task<bool> Delete(Guid id);
        Task<AddressDto> Get(Guid id);
        Task<IEnumerable<AddressDto>> GetAll();
        Task<IEnumerable<AddressDto>> GetAllDeactivated();
        Task<Response<AddressDto>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType);
        Task<Response<AddressDto>> GetAllPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType);
        Task<AddressDto> GetDeativated(Guid id);
        Task<bool> Update(Guid id, CreateAddressDto address);
    }
}