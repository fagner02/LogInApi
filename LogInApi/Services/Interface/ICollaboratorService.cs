using System.Threading.Tasks;
using System.Collections.Generic;
using LogInApi.Models;
using LogInApi.Dtos;
using LogInApi.Enums;


namespace LogInApi.Services {
    public interface ICollaboratorService {
        Task Create(CollaboratorDto Collaborator);
        Task<bool> Deactivate(string cpf);
        Task<bool> Delete(string cpf);
        Task<IEnumerable<CollaboratorDto>> GetAll();
        Task<IEnumerable<CollaboratorDto>> GetAllDeactivated();
        Task<Response<CollaboratorDto>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<Response<CollaboratorDto>> GetAllPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<CollaboratorDto> GetByCpf(string cpf);
        Task<CollaboratorDto> GetByCpfDeactivated(string cpf);
        Task<CollaboratorDto> GetByName(string fullName);
        Task<CollaboratorDto> GetByNameDeactivated(string fullName);
        Task<bool> Update(string cpf, UpdateCollaboratorDto Collaborator);
    }
}