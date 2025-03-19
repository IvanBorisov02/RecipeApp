namespace RecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }          // Заглавие на рецептата
        public string Category { get; set; }       // Категория (напр. закуска, обяд, вечеря, десерт)
        public string Instructions { get; set; }   // Инструкции за приготвяне
        public int PreparationTime { get; set; }   // Време за приготвяне (в минути)
        public double Rating { get; set; }         // Рейтинг (напр. от 1 до 5)
        public string ImagePath { get; set; }        // Път към снимка (по желание)
        public List<Ingredient> Ingredients { get; set; } = new(); // Списък със съставки
    }
}
