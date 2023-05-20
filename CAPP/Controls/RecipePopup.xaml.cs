using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class RecipePopup : Popup
{
    public readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create(nameof(EntryPlaceholder), typeof(string), typeof(RecipePopup), "¬ведите название");

    public string EntryPlaceholder
    {
        get => (string)GetValue(EntryPlaceholderProperty);
        set => SetValue(EntryPlaceholderProperty, value);
    }

    public RecipePopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close(PopupRecipe.Text);
    }
}