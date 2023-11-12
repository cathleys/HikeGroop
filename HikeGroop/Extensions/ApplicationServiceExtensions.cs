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

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.Configure<IPInfoSettings>(config.GetSection("IPInfoSettings"));
            services.AddScoped<IPhotoService, PhotoService>();

            services.AddCloudscribePagination();

            return services;
        }
    }
}
