namespace CAPP;

public partial class HeightPage : ContentPage
{

    public HeightPage()
    {
		InitializeComponent();
	}
    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalPage(), false);
    }

    /*private void heightChanged(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(height.Text))
        {
            if (Convert.ToDouble(height.Text) > 100 && Convert.ToDouble(height.Text) < 270)
            {
                GoNextButton.IsEnabled = true;
                GoNextButton.Background = SolidColorBrush.White;
                GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
                FirstLabel.Text = "";
                border.StrokeThickness = 0;
            }
            else
            {
                GoNextButton.IsEnabled = false;
                GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474")).Color;
                GoNextButton.TextColor = SolidColorBrush.White.Color;
                if (Convert.ToDouble(height.Text) >= 270)
                {
                    FirstLabel.Text = "Рост слишком большой";
                }
                else
                {
                    FirstLabel.Text = "Рост слишком маленький";
                }
                border.StrokeThickness = 1;
            }
        }
        else
        {
            FirstLabel.Text = "Поле не может быть пустым";
            border.StrokeThickness = 1;
        }
    }*/
}

