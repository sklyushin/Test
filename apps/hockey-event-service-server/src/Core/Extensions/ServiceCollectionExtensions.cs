using HockeyEventService.APIs;

namespace HockeyEventService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IChatRoomsService, ChatRoomsService>();
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IPlacesService, PlacesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
