using FLTR.Providers;
using FLTR.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FLTR.IoC
{
    public static class CompositionRoot
    {
        public static void SetupDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILatexService, LatexService>();
            services.AddTransient<ICommandShellProvider, CommandShellProvider>();
            services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
            services.AddTransient<IDocumentProvider, DocumentProvider>();
            services.AddTransient<IEnvironmentProvider, EnvironmentProvider>();
            services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        }
    }
}