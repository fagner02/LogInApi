using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CollaboratorDto>> GetAll() {
            return _mapper.Map<IEnumerable<CollaboratorDto>>(
                (await _collaborator.GetAll()).ToList().Where(x => x.IsActive == true)
            );
        }

        public async Task<IEnumerable<CollaboratorDto>> GetAllDeactivated() {
            return _mapper.Map<IEnumerable<CollaboratorDto>>(
                (await _collaborator.GetAll()).ToList().Where(x => x.IsActive == false)
            );
        }

        public async Task<Response<CollaboratorDto>> GetAllPaged(
            int pageNumber,
            int pageSize,
            OrderCollaboratorColumn orderColumn,
            OrderType orderType
        ) {
            var result = await _collaborator.GetAllPaged(
                pageNumber, pageSize, orderColumn, orderType);
            Response<CollaboratorDto> res = new(result, _mapper.Map<IEnumerable<CollaboratorDto>>(result));
            return res;
        }

        public async Task<Response<CollaboratorDto>> GetAllDeactivatedPaged(
            int pageNumber,
            int pageSize,
            OrderCollaboratorColumn orderColumn,
            OrderType orderType
        ) {
            var result = await _collaborator.GetAllDeactivatedPaged(
                pageNumber, pageSize, orderColumn, orderType);
            Response<CollaboratorDto> res = new(result, _mapper.Map<IEnumerable<CollaboratorDto>>(result));
            return res;
        }

        public async Task Create(CollaboratorDto collaborator) {
            if (Validation.ValidateAge(collaborator.BirthDate)) {
                throw new Exception("Collaborator must be 18 years old or older.");
            }
            if (Validation.ValidateCpf(collaborator.Cpf)) {
                throw new Exception("Invalid CPF.");
            }
            await _collaborator.Create(_mapper.Map<Collaborator>(collaborator));
        }

        public async Task<CollaboratorDto> GetByCpf(string cpf) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp != null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByCpfDeactivated(string cpf) {
            Collaborator temp = await _collaborator.GetDeactivated(x => x.Cpf == cpf);
            if (temp != null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByName(string fullName) {
            Collaborator temp = await _collaborator.Get(x => x.FullName == fullName);
            if (temp != null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<CollaboratorDto> GetByNameDeactivated(string fullName) {
            Collaborator temp = await _collaborator.GetDeactivated(x => x.FullName == fullName);
            if (temp != null) {
                return null;
            }
            return _mapper.Map<CollaboratorDto>(temp);
        }

        public async Task<bool> Update(string cpf, UpdateCollaboratorDto Collaborator) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp == null) {
                return false;
            }
            temp = _mapper.Map<Collaborator>(Collaborator);
            temp.Cpf = cpf;
            await _collaborator.Update(temp);
            return true;
        }

        public async Task<bool> Delete(string cpf) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
            if (temp == null) {
                return false;
            }
            await _collaborator.Delete(temp);
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
    }
}