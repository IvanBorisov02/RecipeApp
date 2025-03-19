using RecipeApp;
using RecipeApp.Services;

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
            });

        // Регистрация на услугите
        builder.Services.AddSingleton<RecipeService>();

        // Регистрация на ViewModel-ите
        builder.Services.AddTransient<RecipeApp.ViewModels.MainViewModel>();
        builder.Services.AddTransient<RecipeApp.ViewModels.RecipeDetailsViewModel>();
        builder.Services.AddTransient<RecipeApp.ViewModels.RecipeFormViewModel>();

        // Регистрация на страниците
        builder.Services.AddTransient<RecipeApp.Views.MainPage>();
        builder.Services.AddTransient<RecipeApp.Views.RecipeDetailsPage>();
        builder.Services.AddTransient<RecipeApp.Views.RecipeFormPage>();

        return builder.Build();
    }
}
