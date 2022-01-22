using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LogInApi.Enums;
using LogInApi.Models;
using X.PagedList;

namespace LogInApi.Repositories {
    public interface ICollaboratorRepository {
        Task<Collaborator> Create(Collaborator collaborator);
        Task<Collaborator> Get(Expression<Func<Collaborator, bool>> predicate);
        Task<IPagedList<Collaborator>> GetAllDeactivatedPaged(int pageNumber = 1, int pageSize = 5, OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, OrderType orderType = OrderType.ASC, OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName, string search = "");
        Task<IPagedList<Collaborator>> GetAllPaged(int pageNumber = 1, int pageSize = 5, OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, OrderType orderType = OrderType.ASC, OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName, string search = "");
        Task<Collaborator> GetDeactivated(Expression<Func<Collaborator, bool>> predicate);
        Task<bool> Update(Collaborator collaborator);
    }
}