using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Enums;

namespace LogInApi.Services {
    public interface IAddressService {
        Task Create(CreateAddressDto address);
        Task<bool> Delete(Guid id);
        Task<AddressDto> Get(Guid id);
        Task<IEnumerable<AddressDto>> GetAll();
        Task<Response<AddressDto>> GetAllPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType);
        Task<bool> Update(Guid id, CreateAddressDto address);
    }
}