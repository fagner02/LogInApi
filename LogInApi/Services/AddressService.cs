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

        public async Task<Response<AddressDto>> GetAllPaged(
            int pageNumber,
            int pageSize,
            OrderAddressColumn orderColumn,
            OrderType orderType,
            OrderAddressColumn searchColumn,
            string search
        ) {
            var result = await _address.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            );
            Response<AddressDto> res = new(result, _mapper.Map<IEnumerable<AddressDto>>(result));
            return res;
        }

        public async Task<Response<AddressDto>> GetAllDeactivatedPaged(
            int pageNumber,
            int pageSize,
            OrderAddressColumn orderColumn,
            OrderType orderType,
            OrderAddressColumn searchColumn,
            string search
        ) {
            var result = await _address.GetAllDeactivatedPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            );
            Response<AddressDto> res = new(result, _mapper.Map<IEnumerable<AddressDto>>(result));
            return res;
        }

        public async Task Create(CreateAddressDto address) {
            await _address.Create(_mapper.Map<Address>(address));
        }

        public async Task<AddressDto> Get(Guid id) {
            Address result = await _address.Get(id);
            if (result == null) {
                return null;
            }
            return _mapper.Map<AddressDto>(result);
        }

        public async Task<AddressDto> GetDeativated(Guid id) {
            Address result = await _address.GetDeactivated(id);
            if (result == null) {
                return null;
            }
            return _mapper.Map<AddressDto>(result);
        }

        public async Task<bool> Update(Guid id, UpdateAddressDto address) {
            Address temp = await _address.Get(id);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == false) {
                return false;
            }
            temp.Street = address.Street ?? temp.Street;
            temp.City = address.City ?? temp.City;
            temp.Number = address.Number ?? temp.Number;
            temp.State = address.State ?? temp.State;
            temp.District = address.District ?? temp.District;
            return await _address.Update(temp);
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

        public async Task<bool> Activate(Guid id) {
            Address temp = await _address.GetDeactivated(id);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == true) {
                return false;
            }
            temp.IsActive = true;
            return await _address.Update(temp);
        }
    }
}