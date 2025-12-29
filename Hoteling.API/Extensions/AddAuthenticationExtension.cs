using Hoteling.API.Options;
using Hoteling.Domain.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Hoteling.Application.Interfaces.IService;

namespace Hoteling.API.Extensions;

public static class AddAuthenticationExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var authOptions = config.GetSection("AuthOptions:Google").Get<AuthOptions>()
                          ?? throw new InvalidOperationException("AuthOptions section is missing in appsettings.json");
        services.Configure<AuthOptions>(config.GetSection("AuthOptions"));
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = authOptions.ClientId;
                options.ClientSecret = authOptions.ClientSecret;

                options.Events.OnCreatingTicket = async context =>
                {
                    var email = context.Principal?.FindFirst(ClaimTypes.Email)?.Value;
                    var name = context.Principal?.FindFirst(ClaimTypes.Name)?.Value;

                    if (string.IsNullOrEmpty(email)) return;

                    var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    var user = await userService.GetUserByEmail(email);

                    if (user == null)
                    {
                        user = new Domain.Entities.User
                        {
                            Id = Guid.NewGuid(),
                            Email = email,
                            UserName = name ?? email,
                            Role = UserRole.Guest
                        };
                        await userService.CreateAsync(user);
                    }

                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity != null && user != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

                        if (!identity.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        }
                    }
                };
            });
        return services;
    }
}
