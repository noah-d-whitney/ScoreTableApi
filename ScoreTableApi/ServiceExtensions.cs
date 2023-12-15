using Microsoft.AspNetCore.Identity;
using ScoreTableApi.Data;
using ScoreTableApi.Models;

namespace ScoreTableApi;

public static class ServiceExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(q => q.User
            .RequireUniqueEmail = true);

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
            services);
        builder.AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
    }
}