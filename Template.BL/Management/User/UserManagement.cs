namespace Template.BL.Management.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

    using Template.BL.Authentication;
    using Template.BL.Management.GenericRepository;
    using Template.DAL;
    using Template.DAL.Enums;

    public class UserManagement : GenericRepository<User>, IUserManagement
    {
        private readonly IAuthentication _authentication;

        private readonly DataContext _dataContext;

        public UserManagement(DataContext dataContext, IAuthentication authentication)
            : base(dataContext)
        {
            this._dataContext = dataContext;
            this._authentication = authentication;
        }

        public async Task CreateUserAsync(string email, string firstname, string? middleName, string lastname, string password)
        {
            var hashedPassword = this._authentication.CreatePasswordHash(password);

            var user = new User(email, firstname, middleName, lastname, hashedPassword);

            await this._dataContext.UserClaims.AddAsync(new UserClaim(user.Id, ClaimType.User)).ConfigureAwait(false);

            await this.CreateAsync(user).ConfigureAwait(false);
        }

        public Task<List<UserClaim>> GetUserWithRolesAsync(string email)
        {
            return this._dataContext.UserClaims.Include(x => x.User).Where(x => x.User.Email == email).ToListAsync();
        }
    }
}