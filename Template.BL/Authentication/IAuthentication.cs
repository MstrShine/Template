namespace Template.BL.Authentication
{
    using System.Threading.Tasks;

    public interface IAuthentication
    {
        Task<bool> LoginAsync(string username, string password);

        string CreatePasswordHash(string password);

        bool ValidatePassword(string savedPassword, string inputtedPassword);
    }
}
