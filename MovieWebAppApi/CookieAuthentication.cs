using Microsoft.AspNetCore.Authentication.Cookies;

namespace MovieWebAppApi
{
    public class CookieAuthentication
    {
        public static void ConfigureCookieAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/access-denied";
                });
        }
    }
}
