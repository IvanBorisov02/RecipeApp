using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeApp.Models;
using RecipeApp.Services;
using System.Collections.ObjectModel;

namespace RecipeApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;

        [ObservableProperty]
        private ObservableCollection<Recipe> recipes;

        public MainViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
            Recipes = new ObservableCollection<Recipe>(_recipeService.GetAllRecipes());
        }

        [RelayCommand]
        private async Task AddRecipe()
        {
            // Навигиране към RecipeFormPage без ID за нов запис
            await Shell.Current.GoToAsync($"{nameof(RecipeApp.Views.RecipeFormPage)}");
        }

        [RelayCommand]
        private async Task EditRecipe(int recipeId)
        {
            // Навигиране към RecipeFormPage с параметър за редакция
            await Shell.Current.GoToAsync($"{nameof(RecipeApp.Views.RecipeFormPage)}?RecipeId={recipeId}");
        }

        [RelayCommand]
        private async Task DeleteRecipe(int recipeId)
        {
            _recipeService.DeleteRecipe(recipeId);
            RefreshData();
            await Shell.Current.DisplayAlert("Success", "Recipe deleted", "OK");
        }

        [RelayCommand]
        private async Task ShowDetails(int recipeId)
        {
            await Shell.Current.GoToAsync($"{nameof(RecipeApp.Views.RecipeDetailsPage)}?RecipeId={recipeId}");
        }

        public void RefreshData()
        {
            Recipes = new ObservableCollection<Recipe>(_recipeService.GetAllRecipes());
        }
    }
}
