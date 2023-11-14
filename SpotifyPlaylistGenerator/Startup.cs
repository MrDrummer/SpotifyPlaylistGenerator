using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Npgsql;
using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Core.Services;
using SpotifyPlaylistGenerator.DB;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Services;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify;
using SpotifyPlaylistGenerator.Spotify.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Services;

namespace SpotifyPlaylistGenerator;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Configuration["HOST"],
            Database = Configuration["DATABASE"],
            Username = Configuration["USERNAME"],
            Password = Configuration["PASSWORD"],
            IncludeErrorDetail = true
        };
        // var connectionString = $"Host={Configuration["HOST"]};Database={Configuration["DATABASE"]};Username={Configuration["USERNAME"]};Password={Configuration["PASSWORD"]};";

        services.AddDbContext<SpotifyDbContext>(options =>
            options.UseNpgsql(builder.ConnectionString)
                .EnableSensitiveDataLogging());
        
        services.AddHttpContextAccessor();
        services.AddSingleton(SpotifyClientConfig.CreateDefault());
        services.AddTransient<SpotifyClientBuilder>();
        services.AddScoped<ISpotifyServiceHolder, SpotifyServiceHolder>();
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddScoped<DbContext, SpotifyDbContext>();
        
        services.AddScoped<IPlaylistService, PlaylistService>();
        services.AddScoped<IDbPlaylistService, DbPlaylistService>();
        services.AddScoped<ISpotifyPlaylistService, SpotifyPlaylistService>();

        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IDbTrackService, DbTrackService>();
        services.AddScoped<ISpotifyTrackService, SpotifyTrackService>();

        services.AddScoped<IDbAppUserPlaylistService, DbAppUserPlaylistService>();

        services.AddScoped<IDbAlbumService, DbAlbumService>();
        services.AddScoped<IDbArtistService, DbArtistService>();
        services.AddScoped<IDbGenreService, DbGenreService>();

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