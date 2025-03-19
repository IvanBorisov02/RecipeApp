using RecipeApp.ViewModels;

namespace RecipeApp.Views
{
    public partial class RecipeFormPage : ContentPage
    {
        public RecipeFormPage(RecipeFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RecipeFormViewModel vm)
            {
                vm.LoadRecipe();
            }
        }
    }
}
