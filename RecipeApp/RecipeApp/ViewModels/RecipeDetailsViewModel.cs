using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp.Models;
using RecipeApp.Services;

namespace RecipeApp.ViewModels
{
    [QueryProperty(nameof(RecipeId), "RecipeId")]
    public partial class RecipeDetailsViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;

        [ObservableProperty]
        private int recipeId;

        [ObservableProperty]
        private Recipe recipe;

        public RecipeDetailsViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public void LoadRecipe()
        {
            Recipe = _recipeService.GetRecipeById(RecipeId);
        }
    }
}
