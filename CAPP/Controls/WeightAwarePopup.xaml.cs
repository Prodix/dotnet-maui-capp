using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class WeightAwarePopup : Popup
{
    public readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create(nameof(EntryPlaceholder), typeof(string), typeof(WeightPopup), "¬ведите вес");
    
    bool IsOk { get; set; } = false;

    public string EntryPlaceholder
    {
        get => (string)GetValue(EntryPlaceholderProperty);
        set => SetValue(EntryPlaceholderProperty, value);
    }


    public WeightAwarePopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close(IsOk);
    }

    private void ButtonNoClicked(object sender, EventArgs e)
    {
        IsOk = false;
        ClosePopup(sender, e);
    }

    private void ButtonOkClicked(object sender, EventArgs e)
    {
        IsOk = true;
        ClosePopup(sender, e);
    }
}