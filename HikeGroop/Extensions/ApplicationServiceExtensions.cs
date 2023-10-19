using HikeGroop.Interfaces;
using HikeGroop.Repositories;

namespace HikeGroop.Extensions
{
    public static class ApplicationServiceExtensions
    {
       

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

           services.AddScoped<IDestinationRepository, DestinationRepository>();
           services.AddScoped<IGroupRepository, GroupRepository>();
            return services;
        }
    }
}
