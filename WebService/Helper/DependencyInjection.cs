using Data.Repository;
using Service;
using Webservice.Helper;

namespace Webservice.Helper
{
    public class DependencyInjection
    {
        public DependencyInjection() { }
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();
            services.AddAutoMapper(typeof(Service.Helper.Mapper).Assembly);
        }
    }
}