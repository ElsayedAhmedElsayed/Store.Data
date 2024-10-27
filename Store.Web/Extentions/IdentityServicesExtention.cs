using Microsoft.AspNetCore.Identity;
using Store.Data.Context;
using Store.Data.Enity.IdentityEntity;

namespace Store.Web.Extentions
{
    public static class IdentityServicesExtention
    {
      public static IServiceCollection AddIdentityService (this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.AddSignInManager<SignInManager<AppUser>> ();

            services.AddAuthentication();

            return services;
        }

    }
}
