using CAPP.Controls;
using CommunityToolkit.Maui.Views;

namespace CAPP;

public partial class HeightPage : ContentPage
{
    string username;
    string email;
    string password;
    bool IsWithoutRegister;

    public HeightPage(string username = "default", string email = "default", string password = "default", bool IsWithoutRegister = false)
    {
		InitializeComponent();
        this.username = username;
        this.email = email;
        this.password = password;
        this.IsWithoutRegister = IsWithoutRegister;
        var gestureRecognizer = new TapGestureRecognizer();
        gestureRecognizer.Tapped += (s, e) => DisplayPopup(s, e);
        ((Frame)HeightEntry.FindByName("ClickableFrame")).GestureRecognizers.Add(gestureRecognizer);
    }

    private async Task DisplayPopup(object sender, TappedEventArgs e)
    {
        var popup = new WeightPopup();
        popup.EntryPlaceholder = "Введите рост";
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
        await Navigation.PushAsync(new GoalPage(HeightEntry.Value, username, email, password, IsWithoutRegister));
    }

    
}

