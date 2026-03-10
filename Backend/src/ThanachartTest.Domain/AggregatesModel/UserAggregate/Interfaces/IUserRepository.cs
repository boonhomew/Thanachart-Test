using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.UserAggregate.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(Guid id);
        Task AddUserAsync(User user);
        Task<bool> UsernameExistsAsync(string username);
    }
}
