namespace CAPP;

public partial class GoalPage : ContentPage
{
    int heightValue = 0;

	public GoalPage(int height)
	{
		InitializeComponent();
        heightValue = height;
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

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        if (CheckOne.IsChecked)
        {
            await Navigation.PushAsync(new WeightPage(true, 1, heightValue), false);
        }
        else if (CheckTwo.IsChecked)
        {
            await Navigation.PushAsync(new WeightPage(true, 2, heightValue), false);
        }
        else
            await Navigation.PushAsync(new WeightPage(false, 0, heightValue), false);

    }
}

