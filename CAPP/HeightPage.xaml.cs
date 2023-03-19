using CAPP.Controls;
using CommunityToolkit.Maui.Views;

namespace CAPP;

public partial class HeightPage : ContentPage
{

    public HeightPage()
    {
		InitializeComponent();
        var gestureRecognizer = new TapGestureRecognizer();
        gestureRecognizer.Tapped += (s, e) => DisplayPopup(s, e);
        ((Frame)HeightEntry.FindByName("ClickableFrame")).GestureRecognizers.Add(gestureRecognizer);
    }

    private async Task DisplayPopup(object sender, TappedEventArgs e)
    {
        var popup = new WeightPopup();
        object? result = await this.ShowPopupAsync(popup);
        if (result != null && Convert.ToInt32(result) <= HeightEntry.MaximalValue)
        {
            if (Convert.ToInt32(result) >= HeightEntry.MinimalValue)
            {
                HeightEntry.Value = Convert.ToInt32(result);
            }
            else
            {
                HeightEntry.Value = HeightEntry.MinimalValue;
            }
        }
        else if (result != null)
        {
            HeightEntry.Value = HeightEntry.MaximalValue;
        }
    }

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalPage(HeightEntry.Value), false);
    }

    
}

