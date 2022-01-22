using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LogInApi.Repositories;
using LogInApi.Models;
using LogInApi.Dtos;
using LogInApi.Enums;
using AutoMapper;
using LogInApi.Validations;

namespace LogInApi.Services {

    public class CollaboratorService : ICollaboratorService {
        private readonly ICollaboratorRepository _collaborator;
        private readonly IMapper _mapper;

        public CollaboratorService(ICollaboratorRepository Collaborator, IMapper mapper) {
            _collaborator = Collaborator;
            _mapper = mapper;
        }

        public async Task<Response<CollaboratorDto>> GetAllPaged(
            int pageNumber,
            int pageSize,
            OrderCollaboratorColumn orderColumn,
            OrderType orderType,
            OrderCollaboratorColumn searchColumn,
            string search
        ) {
            var result = await _collaborator.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            );
            Response<CollaboratorDto> res = new(result, _mapper.Map<IEnumerable<CollaboratorDto>>(result));
            return res;
        }

        public async Task<Response<CollaboratorDto>> GetAllDeactivatedPaged(
            int pageNumber,
            int pageSize,
            OrderCollaboratorColumn orderColumn,
            OrderType orderType,
            OrderCollaboratorColumn searchColumn,
            string search
        ) {
            var result = await _collaborator.GetAllDeactivatedPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search
            );
            Response<CollaboratorDto> res = new(result, _mapper.Map<IEnumerable<CollaboratorDto>>(result));
            return res;
        }

        public async Task<CollaboratorDto> Create(CreateCollaboratorDto collaborator) {
            Collaborator temp = _mapper.Map<Collaborator>(collaborator);
            if (!Validation.ValidateAge(temp.BirthDate)) {
                throw new Exception("Collaborator must be 18 years old or older.");
            }
            if (!Validation.ValidateCpf(temp.Cpf)) {
                throw new Exception("Invalid CPF.");
            }
            if (!Validation.ValidatePhone(temp.Phone)) {
                throw new Exception("Invalid Phone. Check invalid symbols and whitespaces.");
            }
            if (!Validation.ValidateSex(temp.Sex)) {
                throw new Exception("Invalid Sex character");
            }
            Collaborator result = await _collaborator.Create(temp);
            return _mapper.Map<CollaboratorDto>(result);
        }

        public async Task<CollaboratorDto> GetByCpf(string cpf) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp == null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByCpfDeactivated(string cpf) {
            Collaborator temp = await _collaborator.GetDeactivated(x => x.Cpf == cpf);
            if (temp == null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByName(string fullName) {
            Collaborator temp = await _collaborator.Get(x => x.FullName == fullName);
            if (temp == null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByNameDeactivated(string fullName) {
            Collaborator temp = await _collaborator.GetDeactivated(x => x.FullName == fullName);
            if (temp == null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<bool> Update(string cpf, UpdateCollaboratorDto collaborator) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp == null) {
                return false;
            }
            temp.AddressId = collaborator.AddressId ?? temp.AddressId;
            temp.FullName = collaborator.FullName ?? temp.FullName;
            temp.Phone = collaborator.Phone ?? temp.Phone;
            temp.Sex = collaborator.Sex ?? temp.Sex;
            await _collaborator.Update(temp);
            return true;
        }

        public async Task<bool> Deactivate(string cpf) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == false) {
                return false;
            }
            temp.IsActive = false;
            return await _collaborator.Update(temp);
        }

        public async Task<bool> Activate(string cpf) {
            Collaborator temp = await _collaborator.GetDeactivated(x => x.Cpf == cpf);
            if (temp == null) {
                return false;
            }
            if (temp.IsActive == true) {
                return false;
            }
            temp.IsActive = true;
            return await _collaborator.Update(temp);
        }
    }
}