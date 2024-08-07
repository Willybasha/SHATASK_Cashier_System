using BusinessLayer.Services.ServiceManager;
using CoreLayer.Interfaces;
using InfrastructureLayer.Repository.RepositoryManager;

namespace SHATASK.Extensions
{
    public static class ExtensionsMethods
    {
        public static void ConfiguringServiceManager(this IServiceCollection services)
            => services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfiguiringRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepoManager>();
    }
}
