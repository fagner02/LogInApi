using System;
using System.Threading.Tasks;
using LogInApi.Enums;
using LogInApi.Models;
using X.PagedList;

namespace LogInApi.Repositories {
    public interface IAddressRepository {
        Task Create(Address address);
        Task<Address> Get(Guid id);
        Task<IPagedList<Address>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType, OrderAddressColumn searchColumn, string search);
        Task<IPagedList<Address>> GetAllPaged(int pageNumber, int pageSize, OrderAddressColumn orderColumn, OrderType orderType, OrderAddressColumn searchColumn, string search);
        Task<Address> GetDeactivated(Guid id);
        Task<bool> Update(Address address);
    }
}