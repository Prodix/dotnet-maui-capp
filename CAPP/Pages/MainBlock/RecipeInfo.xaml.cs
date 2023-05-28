using System.Collections.ObjectModel;

namespace CAPP.Pages.MainBlock;

public partial class RecipeInfo : ContentPage
{
    ProductDatabase productDatabase = new ProductDatabase();

    public RecipeInfo(RecipeData data, List<RecipeItem> items, List<RecipeStep> steps)
    {

		InitializeComponent();

        img.SetBinding(Image.SourceProperty, "ImageName");
        img.BindingContext = data;

        Header.SetBinding(Label.TextProperty, "Name");
        Header.BindingContext = data;

        Kcal.SetBinding(Label.TextProperty, "Kcal");
        Kcal.BindingContext = data;

        Protein.SetBinding(Label.TextProperty, "Protein");
        Protein.BindingContext = data;

        Fat.SetBinding(Label.TextProperty, "Fat");
        Fat.BindingContext = data;

        Carb.SetBinding(Label.TextProperty, "Carb");
        Carb.BindingContext = data;


        BindableLayout.SetItemsSource(IngredientsView, items);
        BindableLayout.SetItemsSource(CookingView, steps);
    }
}