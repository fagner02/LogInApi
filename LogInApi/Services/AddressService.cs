using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Repositories;
using LogInApi.Enums;
using AutoMapper;
using System.Linq;

namespace LogInApi.Services {

    public class AddressService : IAddressService {
        private readonly IAddressRepository _address;
        private readonly IMapper _mapper;

        public AddressService(IMapper mapper, IAddressRepository address) {
            _mapper = mapper;
            _address = address;
        }

        public async Task<IEnumerable<AddressDto>> GetAll() {
            return _mapper.Map<IEnumerable<AddressDto>>((await _address.GetAll()).ToList()
                .Where(x => x.IsActive == true));
        }

        public async Task<IEnumerable<AddressDto>> GetAllDeactivated() {
            return _mapper.Map<IEnumerable<AddressDto>>((await _address.GetAll()).ToList()
                .Where(x => x.IsActive == false));
        }

        public async Task<Response<AddressDto>> GetAllPaged(
            int pageNumber,
            int pageSize,
            OrderAddressColumn orderColumn,
            OrderType orderType
        ) {
            var result = await _address.GetAllPaged(pageNumber, pageSize, orderColumn, orderType);
            Response<AddressDto> res = new(result, _mapper.Map<IEnumerable<AddressDto>>(result));
            return res;
        }

        public async Task<Response<AddressDto>> GetAllDeactivatedPaged(
            int pageNumber,
            int pageSize,
            OrderAddressColumn orderColumn,
            OrderType orderType
        ) {
            var result = await _address.GetAllDeactivatedPaged(pageNumber, pageSize, orderColumn, orderType);
            Response<AddressDto> res = new(result, _mapper.Map<IEnumerable<AddressDto>>(result));
            return res;
        }

        public async Task Create(CreateAddressDto address) {
            await _address.Create(_mapper.Map<Address>(address));
        }

        public async Task<AddressDto> Get(Guid id) {
            return _mapper.Map<AddressDto>(await _address.Get(id));
        }

        public async Task<AddressDto> GetDeativated(Guid id) {
            return _mapper.Map<AddressDto>(await _address.GetDeactivated(id));
        }

        public async Task<bool> Update(Guid id, CreateAddressDto address) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == false) {
                return false;
            }
            temp.Street = address.Street;
            temp.City = address.City;
            temp.Number = address.Number;
            temp.State = address.State;
            temp.District = address.District;
            return await _address.Update(temp);
        }

        public async Task<bool> Delete(Guid id) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            return await _address.Delete(temp);
        }

        public async Task<bool> Deactivate(Guid id) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == false) {
                return false;
            }
            temp.IsActive = false;
            return await _address.Update(temp);
        }
    }
}