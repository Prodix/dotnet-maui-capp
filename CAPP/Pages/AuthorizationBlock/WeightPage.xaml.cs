using CAPP.Controls;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace CAPP;

public partial class WeightPage : ContentPage
{
    int heightValue = 0, mode = 0;
    bool popupShown = false;

	public WeightPage(int mode, int heightValue)
	{
        this.mode = mode;
        this.heightValue = heightValue;
        InitializeComponent();
        
        WeightEntry.IsToastEnabled = false;

        var gestureRecognizer = new TapGestureRecognizer();
        gestureRecognizer.Tapped += (s, e) => DisplayPopup(s, e);
        ((Frame)WeightEntry.FindByName("ClickableFrame")).GestureRecognizers.Add(gestureRecognizer);
    }

    private async Task DisplayPopup(object sender, TappedEventArgs e)
    {
        var popup = new WeightPopup();
        object? result = await this.ShowPopupAsync(popup);
        if (result != null && Convert.ToDouble(result) <= WeightEntry.MaximalValue)
        {
            if (Convert.ToDouble(result) >= WeightEntry.MinimalValue)
            {
                WeightEntry.Value = Math.Round(Convert.ToDouble(result), 2);
            }
            else
            {
                WeightEntry.Value = WeightEntry.MinimalValue;
            }
        }
        else if (result != null)
        {
            WeightEntry.Value = WeightEntry.MaximalValue;
        }
    }

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        if (mode == 1)
        {
            if (WeightEntry.Value < 18.5 * (heightValue / 100.0 * (heightValue / 100.0)))
            {
                string text = "Вы ввели слишком маленький вес, советуем вам проконсультироваться с врачом. Продолжить?";
                var awarePopup = new WeightAwarePopup();
                awarePopup.EntryPlaceholder = text;
                object? result = await this.ShowPopupAsync(awarePopup);
                if (result != null && Convert.ToBoolean(result))
                {
                    await Navigation.PushAsync(new WeightPageTwo(mode, heightValue, WeightEntry.Value), false);
                }
            }
            else
            {
                await Navigation.PushAsync(new WeightPageTwo(mode, heightValue, WeightEntry.Value), false);
            }
        }
        else if (mode == 2)
        {
            if (WeightEntry.Value > 30 * (heightValue / 100.0 * (heightValue / 100.0)))
            {
                string text = "Вы ввели слишком большой вес, советуем вам проконсультироваться с врачом. Продолжить?";
                var awarePopup = new WeightAwarePopup();
                awarePopup.EntryPlaceholder = text;
                object? result = await this.ShowPopupAsync(awarePopup);
                if (result != null && Convert.ToBoolean(result))
                {
                    await Navigation.PushAsync(new WeightPageTwo(mode, heightValue, WeightEntry.Value), false);
                }
            }
            else
            {
                await Navigation.PushAsync(new WeightPageTwo(mode, heightValue, WeightEntry.Value), false);
            }
        }
        else
            await Navigation.PushAsync(new GenderPage(mode, heightValue, WeightEntry.Value));

    }

}

