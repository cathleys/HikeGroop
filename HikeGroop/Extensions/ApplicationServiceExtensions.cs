using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.Repositories;
using HikeGroop.Services;

namespace HikeGroop.Extensions
{
    public static class ApplicationServiceExtensions
    {
       

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

           services.AddScoped<IDestinationRepository, DestinationRepository>();
           services.AddScoped<IGroupRepository, GroupRepository>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();

            return services;
        }
    }
}
