using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceHelper.Requests;

namespace ServiceHelper
{
    public static class Startup
    {
        public static void WebAuthSetup(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = CookieAuthenticationDefaults.LoginPath;
                    options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;
                }
            ); 

            services.AddAuthorization();

            #region Nginx support

            //nginx support 
            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
            //                               ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            //    // Only loopback proxies are allowed by default.
            //    // Clear that restriction because forwarders are enabled by explicit 
            //    // configuration.
            //    options.KnownNetworks.Clear();
            //    options.KnownProxies.Clear();
            //});

            #endregion

        }

        public static void ApiAuthSetup(this IServiceCollection services, string signingKey)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

            services.AddAuthorization();
        }
        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void HttpClientSetup(this IServiceCollection services, string apiBaseUrl)
        {
            services.AddHttpClient<IRequestManager, RequestManager>((serviceCollection, scope) =>
                {
                    scope.BaseAddress = new Uri(apiBaseUrl,
                        UriKind.Absolute);
                    scope.Timeout = new TimeSpan(0, 1, 0);
                });
        }
    }
}
