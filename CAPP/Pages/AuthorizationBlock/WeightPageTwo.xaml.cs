using CAPP.Controls;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace CAPP;

public partial class WeightPageTwo : ContentPage
{
    int heightValue = 0, mode = 0;
    double intakeCoefficient = 0, curWeightValue = 0;


    public WeightPageTwo(int mode, int heightValue, double weightValue)
	{
        curWeightValue = weightValue;
        this.mode = mode;
        this.heightValue = heightValue;

        InitializeComponent();

        //WeightEntry.IsLoadEvent = false;
        
        //var gestureRecognizer = new TapGestureRecognizer();

        //if (mode == 1)
        //{
        //    WeightEntry.MaximalValue = weightValue;
        //    WeightEntry.MaximumText = "При похудении вес должен быть меньше текущего";
        //    WeightEntry.IsMinimumToastEnabled = false;
        //}
        //else if (mode == 2)
        //{
        //    WeightEntry.MinimalValue = weightValue;
        //    WeightEntry.MinimumText = "При наборе массы вес должен быть больше текущего";
        //    WeightEntry.IsMaximumToastEnabled = false;
        //}

        //gestureRecognizer.Tapped += (s, e) => DisplayPopup(s, e);
        //((Frame)WeightEntry.FindByName("ClickableFrame")).GestureRecognizers.Add(gestureRecognizer);
    }

    private void OnTapOne(object sender, TappedEventArgs e)
    {
        CheckTwo.IsChecked = false;
        CheckThree.IsChecked = false;
        CheckFour.IsChecked = false;
        if (!CheckOne.IsChecked)
            CheckOne.IsChecked = true;
        else
            CheckOne.IsChecked = false;
    }

    private void OnTapTwo(object sender, TappedEventArgs e)
    {
        CheckOne.IsChecked = false;
        CheckThree.IsChecked = false;
        CheckFour.IsChecked = false;
        if (!CheckTwo.IsChecked)
            CheckTwo.IsChecked = true;
        else
            CheckTwo.IsChecked = false;
    }

    private void OnTapThree(object sender, TappedEventArgs e)
    {
        CheckOne.IsChecked = false;
        CheckTwo.IsChecked = false;
        CheckFour.IsChecked = false;
        if (!CheckThree.IsChecked)
            CheckThree.IsChecked = true;
        else
            CheckThree.IsChecked = false;
    }

    private void OnTapFour(object sender, TappedEventArgs e)
    {
        CheckOne.IsChecked = false;
        CheckTwo.IsChecked = false;
        CheckThree.IsChecked = false;
        if (!CheckFour.IsChecked)
            CheckFour.IsChecked = true;
        else
            CheckFour.IsChecked = false;
    }

    private void CheckChanged(object sender, EventArgs e)
    {
        if (((InputKit.Shared.Controls.CheckBox)sender).IsChecked)
        {
            GoNextButton.IsEnabled = true;
            GoNextButton.TextColor = Color.FromArgb("#F83D7F");
            GoNextButton.Background = SolidColorBrush.White;
        }
        else
        {
            GoNextButton.IsEnabled = false;
            GoNextButton.TextColor = SolidColorBrush.White.Color;
            GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474"));
        }
    }

    //private async Task DisplayPopup(object sender, TappedEventArgs e)
    //{
    //    var popup = new WeightPopup();
    //    object? result = await this.ShowPopupAsync(popup);

    //    if (result != null && Convert.ToDouble(result) <= WeightEntry.MaximalValue)
    //    {
    //        if (Convert.ToDouble(result) >= WeightEntry.MinimalValue)
    //        {
    //            WeightEntry.Value = Convert.ToDouble(result);
    //        }
    //        else
    //        {
    //            WeightEntry.Value = WeightEntry.MinimalValue;
    //        }
    //    }
    //    else if (result != null)
    //    {
    //        WeightEntry.Value = WeightEntry.MaximalValue;
    //    }
    //}

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        if (CheckOne.IsChecked)
        {
            intakeCoefficient = 1.2;
        }
        else if (CheckTwo.IsChecked)
        {
            intakeCoefficient = 1.375;
        }
        else if (CheckThree.IsChecked)
        {
            intakeCoefficient = 1.55;
        }
        else
        {
            intakeCoefficient = 1.725;
        }

        await Navigation.PushAsync(new GenderPage(mode, heightValue, curWeightValue, intakeCoefficient), false);
    }
}

