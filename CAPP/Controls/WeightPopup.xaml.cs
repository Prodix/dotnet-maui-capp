using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class WeightPopup : Popup
{
    public readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create(nameof(EntryPlaceholder), typeof(string), typeof(WeightPopup), "������� ���");

    public string EntryPlaceholder
    {
        get => (string)GetValue(EntryPlaceholderProperty);
        set => SetValue(EntryPlaceholderProperty, value);
    }

    public WeightPopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close(PopupWeight.Text);
    }
}