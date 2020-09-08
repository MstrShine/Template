namespace Template.BL
{
    using Microsoft.Extensions.DependencyInjection;

    using Template.BL.Authentication;
    using Template.BL.Management.User;

    public static class DiConfig
    {
        private static bool _isInitialized;

        // Extension on IServiceCollection to configure dependencies from the BL
        public static void ConfigureBL(this IServiceCollection serviceCollection)
        {
            if (_isInitialized)
            {
                return;
            }

            ConfigureManagementDi(serviceCollection);

            serviceCollection.AddScoped<IAuthentication, Authentication.Authentication>();

            _isInitialized = true;
        }

        // Configure the Management map of the BL
        public static void ConfigureManagementDi(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserManagement, UserManagement>();
        }
    }
}