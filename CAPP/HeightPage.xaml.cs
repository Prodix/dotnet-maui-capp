namespace CAPP;

public partial class HeightPage : ContentPage
{

    double heightValue = 0;

    public HeightPage()
    {
		InitializeComponent();
	}

    private void heightChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(height.Text) && height.Text != ".")
        {
            if (Convert.ToDouble(height.Text.Replace(".", ",")) < 30)
            {
                border.StrokeThickness = 1;
                GoNextButton.Background = new SolidColorBrush(Color.FromRgb(116, 116, 116));
                GoNextButton.IsEnabled = false;
                GoNextButton.TextColor = SolidColorBrush.White.Color;
                FirstLabel.Text = "Слишком маленький рост";
            }
            else if (Convert.ToDouble(height.Text.Replace(".", ",")) > 270){
                border.StrokeThickness = 1;
                GoNextButton.Background = new SolidColorBrush(Color.FromRgb(116, 116, 116));
                GoNextButton.IsEnabled = false;
                GoNextButton.TextColor = SolidColorBrush.White.Color;
                FirstLabel.Text = "Слишком большой рост";
            }
            else
            {
                GoNextButton.IsEnabled = true;
                border.StrokeThickness = 0;
                GoNextButton.TextColor = new SolidColorBrush(Color.FromRgb(248, 61, 127)).Color;
                GoNextButton.Background = SolidColorBrush.White;
                FirstLabel.Text = "";
                heightValue = Convert.ToDouble(height.Text.Replace(".", ","));
            }
        }
        else
        {
            if (height.Text == ".")
            {
                height.Text = "";
                border.StrokeThickness = 1;
            }
            FirstLabel.Text = "Поле не может быть пустым";
        }
    }

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalPage(heightValue), false);
    }

    
}

