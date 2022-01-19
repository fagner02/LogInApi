using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LogInApi.Enums;
using LogInApi.Models;
using X.PagedList;

namespace LogInApi.Repositories {
    public interface ICollaboratorRepository {
        Task Create(Collaborator Collaborator);
        Task<bool> Delete(Collaborator collaborator);
        Task<Collaborator> Get(Expression<Func<Collaborator, bool>> predicate);
        Task<IEnumerable<Collaborator>> GetAll();
        Task<IPagedList<Collaborator>> GetAllPaged(
            int pageNumber, int pageSize, OrderCollaboratorColumn orderColumn, OrderType orderType);
        Task<bool> Update(Collaborator collaborator);
    }
}