using HikeGroop.Data;
using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.Repositories;
using HikeGroop.Services;
using Microsoft.EntityFrameworkCore;

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
            services.Configure<IPInfoSettings>(config.GetSection("IPInfoSettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddCloudscribePagination();

            return services;
        }
    }
}
