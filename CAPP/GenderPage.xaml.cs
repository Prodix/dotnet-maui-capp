﻿namespace CAPP;

public partial class GenderPage : ContentPage
{

	public GenderPage()
	{
		InitializeComponent();
	}

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BirthPage(), false);
    }

    private void ButtonOn()
    {
        GoNextButton.IsEnabled = true;
        GoNextButton.Background = SolidColorBrush.White;
        GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
    }

    private void ButtonOff()
    {
        GoNextButton.IsEnabled = false;
        GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474")).Color;
        GoNextButton.TextColor = SolidColorBrush.White.Color;
    }

    private void OnTapOne(object sender, TappedEventArgs e)
    {
        if (CheckOne.IsChecked)
        {
            CheckOne.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckOne.IsChecked = true;
            CheckTwo.IsChecked = false;
            ButtonOn();
        }
    }

    private void OnTapTwo(object sender, TappedEventArgs e)
    {
        if (CheckTwo.IsChecked)
        {
            CheckTwo.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckTwo.IsChecked = true;
            CheckOne.IsChecked = false;
            ButtonOn();
        }
    }
}
