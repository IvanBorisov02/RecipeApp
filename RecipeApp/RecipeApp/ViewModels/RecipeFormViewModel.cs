using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeApp.Models;
using RecipeApp.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.ViewModels
{
    [QueryProperty(nameof(RecipeId), "RecipeId")]
    public partial class RecipeFormViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;

        [ObservableProperty]
        private int recipeId;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string category;

        public List<string> Categories { get; } = new List<string>
        {
            "Закуска",
            "Обяд",
            "Вечеря",
            "Десерт"
        };

        [ObservableProperty]
        private string instructions;

        [ObservableProperty]
        private int preparationTime;

        [ObservableProperty]
        private double rating;

        [ObservableProperty]
        private string imagePath;

        [ObservableProperty]
        private ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();

        // Свойства за въвеждане на нова съставка
        [ObservableProperty]
        private string newIngredientName;

        [ObservableProperty]
        private string newIngredientQuantity;

        // Ново свойство за избрана мерна единица
        [ObservableProperty]
        private string newIngredientUnit;

        public List<string> Units { get; } = new List<string>
        {
            "бр.",
            "мл",
            "л",
            "гр.",
            "щ.",
            "с.л.",
            "ч.л.",
            "ч.ч"
        };

        public RecipeFormViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
            // Задаване на default стойност за мерна единица (например първата)
            NewIngredientUnit = Units.FirstOrDefault();
        }

        public void LoadRecipe()
        {
            if (RecipeId > 0)
            {
                var rec = _recipeService.GetRecipeById(RecipeId);
                if (rec != null)
                {
                    Title = rec.Title;
                    Category = rec.Category;
                    Instructions = rec.Instructions;
                    PreparationTime = rec.PreparationTime;
                    Rating = rec.Rating;
                    ImagePath = rec.ImagePath;
                    Ingredients = new ObservableCollection<Ingredient>(rec.Ingredients);
                }
            }
        }

        [RelayCommand]
        private void AddIngredient()
        {
            if (string.IsNullOrWhiteSpace(NewIngredientName))
            {
                Shell.Current.DisplayAlert("Validation Error", "Ingredient name is required.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewIngredientQuantity))
            {
                Shell.Current.DisplayAlert("Validation Error", "Ingredient quantity is required.", "OK");
                return;
            }
            // Допълнително: проверка за отрицателни количества, ако това има смисъл (например, ако се въвежда само число)
            // Ако всичко е наред:
            var ingredient = new Ingredient
            {
                Name = NewIngredientName,
                Quantity = NewIngredientQuantity,
                Unit = NewIngredientUnit
            };
            Ingredients.Add(ingredient);
            NewIngredientName = string.Empty;
            NewIngredientQuantity = string.Empty;
            NewIngredientUnit = Units.FirstOrDefault();
        }


        // Команда за редактиране на съставка
        [RelayCommand]
        private void EditIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                // Попълване на полетата за нова съставка с данните от избраната съставка
                NewIngredientName = ingredient.Name;
                NewIngredientQuantity = ingredient.Quantity;
                NewIngredientUnit = ingredient.Unit;
                // Премахваме съставката от списъка, за да може след това да бъде добавена като обновена
                Ingredients.Remove(ingredient);
            }
        }

        // Команда за изтриване на съставка
        [RelayCommand]
        private void DeleteIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                Ingredients.Remove(ingredient);
            }
        }

        [RelayCommand]
        private async Task SaveRecipe()
        {
            // Валидация на основните полета
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Title is required.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Category is required.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Instructions))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Instructions field cannot be empty.", "OK");
                return;
            }
            if (PreparationTime < 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Preparation time cannot be negative.", "OK");
                return;
            }
            if (Rating < 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Rating cannot be negative.", "OK");
                return;
            }

            // (Опционално) Ако искаш да провериш и за други условия, например, че поне една съставка е добавена:
            if (Ingredients == null || Ingredients.Count == 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "At least one ingredient is required.", "OK");
                return;
            }

            // Ако всички валидации преминат, продължаваме със запазването:
            if (RecipeId > 0)
            {
                var rec = _recipeService.GetRecipeById(RecipeId);
                if (rec != null)
                {
                    rec.Title = Title;
                    rec.Category = Category;
                    rec.Instructions = Instructions;
                    rec.PreparationTime = PreparationTime;
                    rec.Rating = Rating;
                    rec.ImagePath = ImagePath;
                    rec.Ingredients = Ingredients.ToList();
                    _recipeService.UpdateRecipe(rec);
                }
            }
            else
            {
                var newRecipe = new Recipe
                {
                    Title = Title,
                    Category = Category,
                    Instructions = Instructions,
                    PreparationTime = PreparationTime,
                    Rating = Rating,
                    ImagePath = ImagePath,
                    Ingredients = Ingredients.ToList()
                };
                _recipeService.AddRecipe(newRecipe);
                RecipeId = newRecipe.Id;
            }
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        private async Task DeleteRecipe()
        {
            if (RecipeId > 0)
            {
                _recipeService.DeleteRecipe(RecipeId);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
