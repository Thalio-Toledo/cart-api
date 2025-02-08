using carrinho_api.Entities;
using carrinho_api.Mappings;
using carrinho_api.Services;

namespace carrinho_api.AplicationExtensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<WitnessService>();
            services.AddTransient<LocalService>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
