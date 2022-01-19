using System.Threading.Tasks;
using System.Collections.Generic;
using LogInApi.Models;
using LogInApi.Dtos;
using LogInApi.Enums;


namespace LogInApi.Services {
    public interface ICollaboratorService {
        Task Create(CollaboratorDto Collaborator);
        Task<bool> Delete(string cpf);
        Task<IEnumerable<CollaboratorDto>> GetAll();
        Task<Response<CollaboratorDto>> GetAllPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<CollaboratorDto> GetByCpf(string cpf);
        Task<CollaboratorDto> GetByName(string fullName);
        Task<bool> Update(string cpf, UpdateCollaboratorDto Collaborator);
    }
}