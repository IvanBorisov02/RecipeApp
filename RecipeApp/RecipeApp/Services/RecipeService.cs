using RecipeApp.Models;

namespace RecipeApp.Services
{
    public class RecipeService
    {
        private readonly List<Recipe> _recipes;
        private int _currentId = 1;

        public RecipeService()
        {
            _recipes = new List<Recipe>();
            // Можеш да добавиш няколко демонстрационни рецепти тук, ако искаш.
        }

        public List<Recipe> GetAllRecipes() => _recipes;

        public Recipe GetRecipeById(int id) => _recipes.FirstOrDefault(r => r.Id == id);

        public void AddRecipe(Recipe recipe)
        {
            recipe.Id = _currentId++;
            _recipes.Add(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var existing = GetRecipeById(recipe.Id);
            if (existing != null)
            {
                existing.Title = recipe.Title;
                existing.Category = recipe.Category;
                existing.Instructions = recipe.Instructions;
                existing.PreparationTime = recipe.PreparationTime;
                existing.Rating = recipe.Rating;
                existing.ImagePath = recipe.ImagePath;
                existing.Ingredients = recipe.Ingredients;
            }
        }

        public void DeleteRecipe(int id)
        {
            var recipe = GetRecipeById(id);
            if (recipe != null)
            {
                _recipes.Remove(recipe);
            }
        }
    }
}
