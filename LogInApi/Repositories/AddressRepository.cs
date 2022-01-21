using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LogInApi.Contexts;
using LogInApi.Enums;
using LogInApi.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LogInApi.Repositories {

    public class AddressRepository : IAddressRepository {
        private readonly DatabaseContext _data;
        public AddressRepository(DatabaseContext data) {
            _data = data;
        }

        public async Task<IPagedList<Address>> GetAllPaged(
            int pageNumber, int pageSize,
            OrderAddressColumn orderColumn, OrderType orderType
        ) {
            return await _data.Addresses
                .Where("IsActive == true")
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IPagedList<Address>> GetAllDeactivatedPaged(
            int pageNumber, int pageSize,
            OrderAddressColumn orderColumn, OrderType orderType
        ) {
            return await _data.Addresses
                .Where("IsActive == false")
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task Create(Address address) {
            await _data.Addresses.AddAsync(address);
            await _data.SaveChangesAsync();
        }

        public async Task<Address> Get(Guid id) {
            return await _data.Addresses.Where("IsActive == true").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> GetDeactivated(Guid id) {
            return await _data.Addresses.Where("IsActive == false").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Address address) {
            _data.Addresses.Update(address);
            await _data.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Address address) {
            _data.Addresses.Remove(address);
            await _data.SaveChangesAsync();
            return true;
        }
    }
}