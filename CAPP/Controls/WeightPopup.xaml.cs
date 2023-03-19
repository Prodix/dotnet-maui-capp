using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class WeightPopup : Popup
{
    public WeightPopup()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close(PopupWeight.Text);
    }
}