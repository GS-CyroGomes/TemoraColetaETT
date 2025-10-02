using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Infrastructure.Configuration;
using TemoraColetaETT.Infrastructure.Persistence;
using TemoraColetaETT.Infrastructure.Services;
using TemoraColetaETT.UI.ViewModels;
using TemoraColetaETT.UI.Views;

namespace TemoraColetaETT.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Env.Load();

            LogService.Initialize();
            
            ConfigureServices(builder.Services);

            return builder.Build();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(Env.ApiBaseUrl);
            });

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginView>();
        }
    }
}