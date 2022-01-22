using System;
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

        public async Task<IPagedList<Collaborator>> GetAllPaged(
            int pageNumber = 1,
            int pageSize = 5,
            OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName,
            OrderType orderType = OrderType.ASC,
            OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName,
            string search = ""
        ) {
            string searchQuery = "";
            if (searchColumn == OrderCollaboratorColumn.Address) {
                searchQuery = $"&& Address.Street.Contains(\"{search}\") || " +
                            $"Address.City.Contains(\"{search}\") || " +
                            $"Address.State.Contains(\"{search}\") || " +
                            $"Address.Number.Contains(\"{search}\") || " +
                            $"Address.District.Contains(\"{search}\")";
            } else if (searchColumn == OrderCollaboratorColumn.BirthDate || searchColumn == OrderCollaboratorColumn.AddressId) {
                searchQuery = $"&& {searchColumn}.ToString().Contains(\"{search}\")";
            } else {
                searchQuery = $"&& {searchColumn}.Contains(\"{search}\")";
            }

            return await _data.Collaborators
                .Where($"IsActive == true {searchQuery}")
                .Include(x => x.Address)
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IPagedList<Collaborator>> GetAllDeactivatedPaged(
            int pageNumber = 1,
            int pageSize = 5,
            OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName,
            OrderType orderType = OrderType.ASC,
            OrderCollaboratorColumn searchColumn = OrderCollaboratorColumn.FullName,
            string search = ""
        ) {
            string searchQuery = "";
            if (searchColumn == OrderCollaboratorColumn.Address) {
                searchQuery = $"&& Address.Street.Contains(\"{search}\") || " +
                            $"Address.City.Contains(\"{search}\") || " +
                            $"Address.State.Contains(\"{search}\") || " +
                            $"Address.Number.Contains(\"{search}\") || " +
                            $"Address.District.Contains(\"{search}\")";
            } else if (searchColumn == OrderCollaboratorColumn.BirthDate || searchColumn == OrderCollaboratorColumn.AddressId) {
                searchQuery = $"&& {searchColumn}.ToString().Contains(\"{search}\")";
            } else {
                searchQuery = $"&& {searchColumn}.Contains(\"{search}\")";
            }

            return await _data.Collaborators
                .Where($"IsActive == false {searchQuery}")
                .Include(x => x.Address)
                .OrderBy($"{orderColumn} {orderType}")
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<Collaborator> Create(Collaborator collaborator) {
            await _data.Collaborators.AddAsync(collaborator);
            await _data.SaveChangesAsync();
            return collaborator;
        }

        public async Task<Collaborator> Get(Expression<Func<Collaborator, bool>> predicate) {
            return await _data.Collaborators
                .Where("IsActive == true")
                .Include(x => x.Address)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<Collaborator> GetDeactivated(Expression<Func<Collaborator, bool>> predicate) {
            return await _data.Collaborators
                .Where("IsActive == false")
                .Include(x => x.Address)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> Update(Collaborator collaborator) {
            _data.Collaborators.Update(collaborator);
            await _data.SaveChangesAsync();
            return true;
        }
    }
}