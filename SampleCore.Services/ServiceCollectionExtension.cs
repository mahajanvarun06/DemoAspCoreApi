using SampleCore.Data;
using SampleCore.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void RegisterData(this IServiceCollection services)
        {
            services.AddScoped<IProductData, ProductData>();
            services.AddScoped<IUserData, UserData>();
        }
    }
}
