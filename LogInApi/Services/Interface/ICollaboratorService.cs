using System.Threading.Tasks;
using LogInApi.Models;
using LogInApi.Dtos;
using LogInApi.Enums;

namespace LogInApi.Services {
    public interface ICollaboratorService {
        Task<CollaboratorDto> Create(CreateCollaboratorDto collaborator);
        Task<bool> Deactivate(string cpf);
        Task<Response<CollaboratorDto>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<Response<CollaboratorDto>> GetAllPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<CollaboratorDto> GetByCpf(string cpf);
        Task<CollaboratorDto> GetByCpfDeactivated(string cpf);
        Task<CollaboratorDto> GetByName(string fullName);
        Task<CollaboratorDto> GetByNameDeactivated(string fullName);
        Task<bool> Update(string cpf, UpdateCollaboratorDto collaborator);
    }
}