using CAPP.Controls;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace CAPP;

public partial class WeightPageTwo : ContentPage
{
    int heightValue = 0;
    double weightValue = 0;
    double curWeightValue = 0;
    int mode = 0;
    string username;
    string email;
    string password;

	public WeightPageTwo(int mode, int heightValue, double weightValue, string username, string email, string password)
	{
        curWeightValue = weightValue;
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.username = username;
        this.email = email;
        this.password = password;
        InitializeComponent();

        WeightEntry.IsLoadEvent = false;
        
        var gestureRecognizer = new TapGestureRecognizer();

        if (mode == 1)
        {
            WeightEntry.MaximalValue = weightValue;
            WeightEntry.MaximumText = "При похудении вес должен быть меньше текущего";
            WeightEntry.IsMinimumToastEnabled = false;
        }
        else if (mode == 2)
        {
            WeightEntry.MinimalValue = weightValue;
            WeightEntry.MinimumText = "При наборе массы вес должен быть больше текущего";
            WeightEntry.IsMaximumToastEnabled = false;
        }

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
                WeightEntry.Value = Convert.ToDouble(result);
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
            if (WeightEntry.Value == weightValue)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(WeightEntry.MaximumText, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);

                return;
            }
            if (WeightEntry.Value < 18.5 * (heightValue / 100.0 * (heightValue / 100.0)))
            {
                string text = "Вы ввели слишком маленький вес, советуем вам проконсультироваться с врачом. Продолжить?";
                var awarePopup = new WeightAwarePopup();
                awarePopup.EntryPlaceholder = text;
                object? result = await this.ShowPopupAsync(awarePopup);
                if (result != null && Convert.ToBoolean(result))
                {
                    await Navigation.PushAsync(new GenderPage(mode, heightValue, curWeightValue, username, email, password, WeightEntry.Value), false);
                }
            }
            else
            {
                await Navigation.PushAsync(new GenderPage(mode, heightValue, curWeightValue, username, email, password, WeightEntry.Value), false);
            }
        }
        else if (mode == 2)
        {
            if (WeightEntry.Value == weightValue)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;
                var toast = Toast.Make(WeightEntry.MinimumText, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);

                return;
            }
            if (WeightEntry.Value > 30 * (heightValue / 100.0 * (heightValue / 100.0)))
            {
                string text = "Вы ввели слишком большой вес, советуем вам проконсультироваться с врачом. Продолжить?";
                var awarePopup = new WeightAwarePopup();
                awarePopup.EntryPlaceholder = text;
                object? result = await this.ShowPopupAsync(awarePopup);
                if (result != null && Convert.ToBoolean(result))
                {
                    await Navigation.PushAsync(new GenderPage(mode, heightValue, curWeightValue, username, email, password, WeightEntry.Value), false);
                }
            }
            else
            {
                await Navigation.PushAsync(new GenderPage(mode, heightValue, curWeightValue, username, email, password, WeightEntry.Value), false);
            }
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        WeightEntry.Value = weightValue;
    }
}

