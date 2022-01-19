using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Repositories;
using LogInApi.Enums;
using AutoMapper;

namespace LogInApi.Services {

    public class AddressService : IAddressService {
        private readonly IAddressRepository _address;
        private readonly IMapper _mapper;

        public AddressService(IMapper mapper, IAddressRepository address) {
            _mapper = mapper;
            _address = address;
        }

        public async Task<IEnumerable<AddressDto>> GetAll() {
            return _mapper.Map<IEnumerable<AddressDto>>(await _address.GetAll());
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

        public async Task Create(CreateAddressDto address) {
            await _address.Create(_mapper.Map<Address>(address));
        }

        public async Task<AddressDto> Get(Guid id) {
            return _mapper.Map<AddressDto>(await _address.Get(id));
        }

        public async Task<bool> Update(Guid id, CreateAddressDto address) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            temp = _mapper.Map<Address>(address);
            temp.Id = id;
            return await _address.Update(temp);
        }

        public async Task<bool> Delete(Guid id) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            return await _address.Delete(temp);
        }
    }
}