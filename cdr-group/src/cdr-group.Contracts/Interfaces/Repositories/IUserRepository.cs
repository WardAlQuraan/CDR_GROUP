using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetWithRolesAsync(Guid id);
        Task<IEnumerable<User>> GetUsersWithRolesAsync();
        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersWithRolesPagedAsync(PagedRequest request);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task<List<UserRole>> GetAllUserRolesAsync(Guid userId);
        Task AddUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(UserRole userRole);
    }
}
