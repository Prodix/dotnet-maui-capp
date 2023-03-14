using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace CAPP;

public partial class BirthPage : ContentPage
{

	public BirthPage()
	{
		InitializeComponent();
	}

    private void CheckValid()
    {
        if (Days.Text.Length == 2 && Months.Text.Length == 2 && Years.Text.Length == 4)
        {
            if (Convert.ToInt32(Days.Text) > 0 && Convert.ToInt32(Months.Text) > 0 && Convert.ToInt32(Years.Text) > DateTime.Now.Year-100 && Convert.ToInt32(Months.Text) <= 12 && Convert.ToInt32(Days.Text) <= DateTime.DaysInMonth(Convert.ToInt32(Years.Text), Convert.ToInt32(Months.Text)) )
            {
                DateTime birthDate = DateTime.Parse($"{Days.Text}/{Months.Text}/{Years.Text}");
                DateTime currentDate = DateTime.Now;

                
                if ((currentDate - birthDate).Days / 365 >= 18)
                {
                    GoNextButton.IsEnabled = true;
                    GoNextButton.Background = new SolidColorBrush(Colors.White);
                    GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
                }
                else
                {
                    GoNextButton.IsEnabled = false;
                    GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474"));
                    GoNextButton.TextColor = new SolidColorBrush(Colors.White).Color;
                }
            }
            else
            {
                GoNextButton.IsEnabled = false;
                GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474"));
                GoNextButton.TextColor = new SolidColorBrush(Colors.White).Color;
            }
        }
        else
        {
            GoNextButton.IsEnabled = false;
            GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474"));
            GoNextButton.TextColor = new SolidColorBrush(Colors.White).Color;
        }
    }

    private void Days_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!Days.Text.Contains('.'))
        {
            if (Days.Text.Length == 2)
                Months.Focus();
            if (!String.IsNullOrEmpty(Months.Text) && !String.IsNullOrEmpty(Years.Text))
                CheckValid();
        }
    }

    private void Months_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!Months.Text.Contains('.'))
        {
            if (Months.Text.Length == 2)
                Years.Focus();
            if (!String.IsNullOrEmpty(Days.Text) && !String.IsNullOrEmpty(Years.Text))
                CheckValid();
        }
    }

    private void Years_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!Years.Text.Contains('.'))
        {
            if (!String.IsNullOrEmpty(Days.Text) && !String.IsNullOrEmpty(Months.Text))
                CheckValid();
        }
    }

}

