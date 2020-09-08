namespace Template.BL.Authentication
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Template.DAL;

    public class Authentication : IAuthentication
    {
        private readonly DataContext _dataContext;

        public Authentication(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await this._dataContext.Users.SingleAsync(x => x.Email == username).ConfigureAwait(false);

            return this.ValidatePassword(user.Password, password);
        }

        public string CreatePasswordHash(string password)
        {
            byte[] salt;

            // Generate salt
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Hash and salt it
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            // Place the string in the byte array
            var hash = pbkdf2.GetBytes(20);

            // Create new byte array to store hashed password + salt (36 because 20+16)
            var hashBytes = new byte[36];

            // Place the salt and the hash on the right places
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the byte arrays to a string for saving
            var hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public bool ValidatePassword(string savedPassword, string inputtedPassword)
        {
            var hashBytes = Convert.FromBase64String(savedPassword);

            // Take salt out of the string
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // hash the user inputted password with the salt 
            var pbkdf2 = new Rfc2898DeriveBytes(inputtedPassword, salt, 10000);

            // Put it in a byte array  to compare it byte by byte
            var hash = pbkdf2.GetBytes(20);

            // Compare result byte by byte
            // start from 16 because 0-15 is the salt
            for (var i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}