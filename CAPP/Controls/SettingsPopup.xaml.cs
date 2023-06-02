using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class SettingsPopup : Popup
{
	int height = 0;
	double weight = 0;
	UserData userData = new UserData();

	public SettingsPopup(double weight, int height)
	{
		this.weight = weight;
		this.height = height;

		InitializeComponent();

		Weight.Text = weight.ToString();
		Height.Text = height.ToString();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if (String.IsNullOrEmpty(Weight.Text) || String.IsNullOrEmpty(Height.Text))
		{
            string text = "Поля не могут быть пустыми";
            await Toast.Make(text, ToastDuration.Short).Show();
			return;
        }

		userData.WeightValue = Math.Round(double.Parse(Weight.Text.Replace(".", ",")), 2);
		userData.HeightValue = Convert.ToInt32(double.Parse(Height.Text.Replace(".", ",")));

		Close(userData);
    }
}