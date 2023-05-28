using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class ProductPopup : Popup
{
    ProductDatabase productDatabase;

	public ProductPopup()
	{
		InitializeComponent();
        BindingContext = this;
        productDatabase = new ProductDatabase();
    }

    private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrWhiteSpace(((Entry)sender).Text))
        {
            List<ProductData> products = await productDatabase.FindProducts(((Entry)sender).Text);
            List<RecipeData> recipes = await productDatabase.FindRecipes(((Entry)sender).Text);

            foreach (RecipeData recipe in recipes)
            {
                products.Add(new ProductData
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Fat = recipe.Fat,
                    Carb = recipe.Carb,
                    Kcal = recipe.Kcal,
                    Protein = recipe.Protein,
                    Type = "Recipe"
                });
            }

            SearchCollection.ItemsSource = products;

        }
        else
        {
            SearchCollection.ItemsSource = new List<ProductData>();
        }
    }

    private void SearchCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Close(SearchCollection.SelectedItem);
    }
}