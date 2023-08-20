using concert_svc.Services;

namespace concert_svc.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IConcertService, ConcertService>();
            services.AddScoped<ITicketService, TicketService>();
            // Add other service registrations here

            return services;
        }
    }
}
