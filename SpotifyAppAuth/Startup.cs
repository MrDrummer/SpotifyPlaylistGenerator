using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web;
using SpotifyAppAuth.Services;

namespace SpotifyAppAuth;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(SpotifyClientConfig.CreateDefault());
        services.AddTransient<SpotifyClientBuilder>();
        services.AddScoped<ISpotifyServiceHolder, SpotifyServiceHolder>();
        services.AddScoped<PlaylistService>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Spotify", policy =>
            {
                policy.AuthenticationSchemes.Add("Spotify");
                policy.RequireAuthenticatedUser();
            });
        });

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
            })
            .AddSpotify(options =>
            {
                options.ClientId = Configuration["SPOTIFY_CLIENT_ID"];
                options.ClientSecret = Configuration["SPOTIFY_CLIENT_SECRET"];
                options.CallbackPath = "/api/auth/callback";
                options.SaveTokens = true;
                
                var scopes = new List<string>
                {
                    Scopes.PlaylistReadPrivate,
                    Scopes.PlaylistReadCollaborative,
                    Scopes.AppRemoteControl,
                    Scopes.PlaylistModifyPrivate,
                    Scopes.PlaylistModifyPublic,
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserReadPlaybackState,
                    Scopes.UserReadPlaybackPosition,
                    Scopes.UserReadRecentlyPlayed,
                    Scopes.UserFollowRead
                };
                
                options.Scope.Add(string.Join(",", scopes));

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request,
                            HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        Console.WriteLine(user);
                    }
                };
            });
        
        services.AddHttpClient();
        
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/", "Spotify");
            });

        services.AddServerSideBlazor();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            // app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapControllers();
            endpoints.MapRazorPages();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}