using RecipeApp.ViewModels;

namespace RecipeApp.Views
{
    public partial class RecipeDetailsPage : ContentPage
    {
        public RecipeDetailsPage(RecipeDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RecipeDetailsViewModel vm)
            {
                vm.LoadRecipe();
            }
        }
    }
}
