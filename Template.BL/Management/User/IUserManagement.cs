namespace Template.BL.Management.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Template.BL.Management.GenericRepository;
    using Template.DAL.Models;

    public interface IUserManagement : IGenericRepository<User>
    {
        Task CreateUserAsync(string email, string firstname, string? middleName, string lastname, string password);

        Task<List<UserClaim>> GetUserWithRolesAsync(string email);
    }
}