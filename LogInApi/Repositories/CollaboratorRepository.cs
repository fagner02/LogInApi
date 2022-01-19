using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LogInApi.Contexts;
using LogInApi.Enums;
using LogInApi.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LogInApi.Repositories {

    public class CollaboratorRepository : ICollaboratorRepository {
        private readonly DatabaseContext _data;
        public CollaboratorRepository(DatabaseContext data) {
            _data = data;
        }

        public async Task<IEnumerable<Collaborator>> GetAll() {
            return await _data.Collaborators.ToListAsync();
        }

        public async Task<IPagedList<Collaborator>> GetAllPaged(
            int pageNumber, int pageSize,
            OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName, OrderType orderType = OrderType.ASC
        ) {
            return await _data.Collaborators
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IPagedList<Collaborator>> GetAllDeactivatedPaged(
            int pageNumber, int pageSize,
            OrderCollaboratorColumn orderColumn, OrderType orderType
        ) {
            return await _data.Collaborators
                .Where("IsActive == false")
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task Create(Collaborator Collaborator) {
            await _data.Collaborators.AddAsync(Collaborator);
            await _data.SaveChangesAsync();
        }

        public async Task<Collaborator> Get(Expression<Func<Collaborator, bool>> predicate) {
            return await _data.Collaborators.FirstOrDefaultAsync(predicate);
        }

        public async Task<Collaborator> GetDeactivated(Expression<Func<Collaborator, bool>> predicate) {
            return await _data.Collaborators.Where("IsActive == false").FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> Update(Collaborator collaborator) {
            _data.Collaborators.Update(collaborator);
            await _data.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Collaborator collaborator) {
            _data.Collaborators.Remove(collaborator);
            await _data.SaveChangesAsync();
            return true;
        }
    }
}