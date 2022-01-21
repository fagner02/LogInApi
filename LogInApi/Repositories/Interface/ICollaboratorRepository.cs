using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LogInApi.Enums;
using LogInApi.Models;
using X.PagedList;

namespace LogInApi.Repositories {
    public interface ICollaboratorRepository {
        Task<Collaborator> Create(Collaborator collaborator);
        Task<bool> Delete(Collaborator collaborator);
        Task<Collaborator> Get(Expression<Func<Collaborator, bool>> predicate);
        Task<IPagedList<Collaborator>> GetAllDeactivatedPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<IPagedList<Collaborator>> GetAllPaged(int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, OrderType orderType = OrderType.ASC);
        Task<Collaborator> GetDeactivated(Expression<Func<Collaborator, bool>> predicate);
        Task<bool> Update(Collaborator collaborator);
    }
}