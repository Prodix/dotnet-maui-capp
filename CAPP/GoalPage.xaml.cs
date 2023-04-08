namespace CAPP;

public partial class GoalPage : ContentPage
{
    int heightValue = 0;
    string username;
    string password;
    string email;

	public GoalPage(int height, string username, string email, string password)
	{
		InitializeComponent();
        heightValue = height;
        this.password = password;
        this.username = username;
        this.email = email;
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
            await Navigation.PushAsync(new WeightPage(1, heightValue, username, email, password));
        }
        else if (CheckTwo.IsChecked)
        {
            await Navigation.PushAsync(new WeightPage(2, heightValue, username, email, password));
        }
        else if (CheckThree.IsChecked)
        {
            await Navigation.PushAsync(new WeightPage(3, heightValue, username, email, password));
        }
        else
            await Navigation.PushAsync(new WeightPage(4, heightValue, username, email, password));

    }
}

