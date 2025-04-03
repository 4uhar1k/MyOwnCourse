using Microsoft.Extensions.Logging;
using MyOwnCourseApiClient.IoC;
using MyOwnCourseApp.Converters;
using MyOwnCourseApp.LocalDatabase;
using MyOwnCourseApp.ViewModels;
using MyOwnCourseApp.Views;

namespace MyOwnCourseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddMOCApiClientService(x => x.ApiBaseAdress = "http://10.0.2.2:5000");
            builder.Services.AddTransient<IdToNameConverter>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SignUpNInViewModel>();
            builder.Services.AddTransient<CourseViewModel>();
            builder.Services.AddTransient<MyCoursesPage>();
            builder.Services.AddTransient<FollowedCoursesPage>();
            builder.Services.AddSingleton<SqlConnectionBase>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
