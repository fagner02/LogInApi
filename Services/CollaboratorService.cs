using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using LogInApi.Repositories;
using LogInApi.Models;
using LogInApi.Dtos;
using LogInApi.Enums;
using AutoMapper;


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
                (await _collaborator.GetAll()).ToList()
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

        public async Task Create(CollaboratorDto Collaborator) {
            await _collaborator.Create(_mapper.Map<Collaborator>(Collaborator));
        }

        public async Task<CollaboratorDto> GetByCpf(string cpf) {
            Collaborator temp = await _collaborator.Get(x => x.Cpf == cpf);
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
    }
}